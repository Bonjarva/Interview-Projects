using Microsoft.AspNetCore.Mvc;
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

    // [HttpGet]
    // public IActionResult Get()
    // {

    //     try
    //     {
    //         using var reader = new StreamReader(_csvPath);
    //         using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

    //         var records = csv.GetRecords<Co2Emissions>().ToList();
    //         return Ok(records);

    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"An error occurred: {ex.Message}");

    //     }
    // }

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
            var records = csv.GetRecords<Top10Percapita>().OrderByDescending(r => r.Percapita).Take(10).ToList();
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
            var records = csv.GetRecords<Top10LifeExpectancy>().OrderByDescending(r => r.LifeExpectancy).Take(10).ToList();
            return Ok(records);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");

        }
    }

    [HttpPost("co2EmissionsAndYearlyChange")]
    public IActionResult GetCo2EmisisionsAndYearlyChange([FromBody] CountryList request)
    {
        try
        {
            string[] requestedCountryCodes;

            if (request.Countries != String.Empty)
            {
                requestedCountryCodes = request.Countries.Split(',');
            }
            else
            {
                return BadRequest("Country codes not provided in headers.");
            }

            using var reader = new StreamReader(_csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            var countries = csv.GetRecords<Co2EmissionsYearlyChange>().Where(country => requestedCountryCodes.Contains(country.Code)).ToList();
            if (countries.Any())
            {
                return Ok(countries);
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

    [HttpPost("totalCo2Emissions")]
    public IActionResult GetTotalCo2Emissions([FromBody] CountryList request)
    {
        try
        {

            string[] requestedCountryCodes;

            if (request.Countries != String.Empty)
            {
                requestedCountryCodes = request.Countries.Split(',');
            }
            else
            {
                return BadRequest("Country codes not provided in headers.");
            }

            using var reader = new StreamReader(_csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            var countries = csv.GetRecords<Co2Emissions>().Where(country => requestedCountryCodes.Contains(country.Code)).ToList();

            if (countries.Any())
            {
                var sumCO2Emissions = countries.Sum(c => c.CO2Emissions);


                return Ok(new TotalCO2Emissions
                {
                    TotalEmissions = sumCO2Emissions
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








