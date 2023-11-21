namespace Veyt_Project;

public class Co2Emissions
{
    //Example csv file data
    //Country, String "New Zealand"
    //Code, "NZL"
    //CO2Emissions, 33276202
    //YearlyChange, -0.14
    //Percapita, 7.14
    //Population, 4659265
    //LifeExpectancy 81.862

    //todo: check data types

    public string? Country { get; set; }

    public string? Code { get; set; }

    public long CO2Emissions { get; set; }

    public float YearlyChange { get; set; }

    public float PerCapita { get; set; }

    public long Population { get; set; }

    public float LifeExpectancy { get; set; }

}
