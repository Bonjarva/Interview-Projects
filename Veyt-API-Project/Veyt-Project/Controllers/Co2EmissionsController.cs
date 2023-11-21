using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

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
                       PerCapita = float.Parse(columns[4]),
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
}