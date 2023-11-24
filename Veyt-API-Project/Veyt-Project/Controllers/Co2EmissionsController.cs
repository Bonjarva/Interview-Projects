using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Veyt_Project.Controllers;

[ApiController]
[Route("[controller]")]
public class Co2EmissionsController : ControllerBase
{
    //Example csv file data
    //Country, String "New Zealand"
    //Code, "NZL"
    //CO2Emissions, 33276202
    //YearlyChange, -0.14
    //Percapita, 7.14
    //Population, 4659265
    //LifeExpectancy 81.862

    private readonly CsvFileRetrievalService _csvFileRetrievalService;

    public Co2EmissionsController(CsvFileRetrievalService csvFileRetrievalService)
    {
        _csvFileRetrievalService = csvFileRetrievalService;
    }

    [HttpGet("health")]
    public async Task<IActionResult> GetStatus()
    {

        await _csvFileRetrievalService.StartAsync(new CancellationToken());

        if (string.IsNullOrEmpty(_csvFileRetrievalService.CsvContent))
        {
            return BadRequest("CSV content is not available.");
        }

        return Ok("Healthy");
    }

    [HttpGet("top10Percapita")]
    public async Task<IActionResult> GetTop10Percapita()
    {
        try
        {
            await _csvFileRetrievalService.StartAsync(new CancellationToken());

            if (string.IsNullOrEmpty(_csvFileRetrievalService.CsvContent))
            {
                return BadRequest("CSV content is not available.");
            }

            using var reader = new StringReader(_csvFileRetrievalService.CsvContent);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            var countries = csv.GetRecords<Top10Percapita>().OrderByDescending(country => country.Percapita).Take(10).ToList();
            return Ok(countries);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");

        }
    }

    [HttpGet("top10LifeExpectancy")]
    public async Task<IActionResult> GetTop10LifeExpectancy()
    {

        try
        {
            await _csvFileRetrievalService.StartAsync(new CancellationToken());

            if (string.IsNullOrEmpty(_csvFileRetrievalService.CsvContent))
            {
                return BadRequest("CSV content is not available.");
            }

            using var reader = new StringReader(_csvFileRetrievalService.CsvContent);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            var countries = csv.GetRecords<Top10LifeExpectancy>().OrderByDescending(country => country.LifeExpectancy).Take(10).ToList();
            return Ok(countries);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");

        }
    }

    [HttpPost("co2EmissionsAndYearlyChange")]
    public async Task<IActionResult> GetCo2EmisisionsAndYearlyChange([FromBody] CountryList request)
    {
        try
        {
            string[] requestedCountryCodes;

            if (request.Countries != String.Empty)
            {
                requestedCountryCodes = request.Countries.Split(',')
                                .Select(country => country.Trim().ToUpper())
                                .ToArray();
            }
            else
            {
                return BadRequest("Country codes not provided in headers.");
            }

            await _csvFileRetrievalService.StartAsync(new CancellationToken());

            if (string.IsNullOrEmpty(_csvFileRetrievalService.CsvContent))
            {
                return BadRequest("CSV content is not available.");
            }

            using var reader = new StringReader(_csvFileRetrievalService.CsvContent);
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
    public async Task<IActionResult> GetTotalCo2Emissions([FromBody] CountryList request)
    {
        try
        {

            string[] requestedCountryCodes;

            if (request.Countries != String.Empty)
            {
                requestedCountryCodes = request.Countries.Split(',')
                                .Select(country => country.Trim().ToUpper())
                                .ToArray();
            }
            else
            {
                return BadRequest("Country codes not provided in headers.");
            }

            await _csvFileRetrievalService.StartAsync(new CancellationToken());

            if (string.IsNullOrEmpty(_csvFileRetrievalService.CsvContent))
            {
                return BadRequest("CSV content is not available.");
            }

            using var reader = new StringReader(_csvFileRetrievalService.CsvContent);
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