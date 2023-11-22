using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Veyt_Project.Controllers;

[ApiController]
[Route("[controller]")]
public class Co2EmissionsController : ControllerBase
{
    private readonly string _csvPath = "CO2-emissions.csv";
    //Example csv file data
    //Country, String "New Zealand"
    //Code, "NZL"
    //CO2Emissions, 33276202
    //YearlyChange, -0.14
    //Percapita, 7.14
    //Population, 4659265
    //LifeExpectancy 81.862

    [HttpGet]
    public IEnumerable<Co2Emissions> Get()
    {

        //pull data out of the csv file ready for processing
        var lines = System.IO.File.ReadAllLines(_csvPath);
        var data = from line in lines.Skip(1)
                   let columns = line.Split(',')
                   select new Co2Emissions
                   {
                       Country = columns[0],
                       Code = columns[1],
                       CO2Emissions = long.Parse(columns[2]),
                       YearlyChange = float.Parse(columns[3]),
                       Percapita = float.Parse(columns[4]),
                       Population = long.Parse(columns[5]),
                       LifeExpectancy = float.Parse(columns[6])
                   };

        return data.ToList();
    }

    [HttpGet("status")]
    public ActionResult<string> GetStatus()
    {
        // Perform health checks here
        if (!System.IO.File.Exists(_csvPath))
        {
            return StatusCode(500, "Input file not found");
        }

        //if all checks pass send back an OK status message.
        return Ok("OK");
    }

    [HttpGet("top10Percapita")]
    public IActionResult GetTop10Percapita()
    {

        try
        {
            using var reader = new StreamReader(_csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            var records = csv.GetRecords<Co2Emissions>().OrderByDescending(r => r.Percapita).Take(10).ToList();
            return Ok(records);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");

        }
    }

    [HttpGet("top10LifeExpectancy")]
    public IActionResult GetTop10LifeExpectancy()
    {

        try
        {
            using var reader = new StreamReader(_csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            var records = csv.GetRecords<Co2Emissions>().OrderByDescending(r => r.LifeExpectancy).Take(10).ToList();
            return Ok(records);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");

        }
    }



    // 2. url/co2EmissionYearlyChange
    //     POST request, given the list of country codes (Need to check what format the list will come in as in parameter and sanitize input)
    //     return Co2 Emissions and YearlyChange for given countries
    // 2. Return CO2Emissions and YearlyChange given a list of country codes (Ex,: can,lux,est)

    [HttpPost("co2EmissionsAndYearlyChange")]
    public IActionResult GetCo2EmisisionsAndYearlyChange([FromBody] CountryList request)
    {
        try
        {

            string[] requestedCountryCodes;

            // Get country codes from the request header
            if (request.Countries != String.Empty)
            {
                // Split the comma-separated list of country codes
                requestedCountryCodes = request.Countries.Split(',');
                //possibly regex for checking its a 3 letter code separated by an optional comma

            }
            else
            {
                return BadRequest("Country codes not provided in headers.");
            }

            using var reader = new StreamReader(_csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            var records = csv.GetRecords<Co2Emissions>().Where(record => requestedCountryCodes.Contains(record.Code)).ToList();
            return Ok(records);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");

        }
    }


    // 4. url/totalEmissions
    //     POST request, given the list of country codes (Need to check what format the list will come in as in parameter and sanitize input)
    //     return total emissions
    [HttpPost("totalCo2Emissions")]
    public IActionResult GetTotalCo2Emissions([FromBody] CountryList request)
    {
        try
        {

            string[] requestedCountryCodes;

            // Get country codes from the request header
            if (request.Countries != String.Empty)
            {
                // Split the comma-separated list of country codes
                requestedCountryCodes = request.Countries.Split(',');
                //possibly regex for checking its a 3 letter code separated by an optional comma

            }
            else
            {
                return BadRequest("Country codes not provided in headers.");
            }

            using var reader = new StreamReader(_csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            var records = csv.GetRecords<Co2Emissions>().Where(record => requestedCountryCodes.Contains(record.Code)).ToList();

            if (records.Any())
            {
                var sumCO2Emissions = records.Sum(c => c.CO2Emissions);

                return Ok(new
                {
                    TotalCO2Emissions = sumCO2Emissions
                });
            }
            else
            {
                return NotFound("No matching countries found.");
            }

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");

        }

    }

}








