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

    public required string Country { get; set; }

    public required string Code { get; set; }

    public long CO2Emissions { get; set; }

    public decimal YearlyChange { get; set; }

    public decimal Percapita { get; set; }

    public long Population { get; set; }

    public decimal LifeExpectancy { get; set; }

}

// public class Co2EmissionsYearlyChangeSubset
// {
//     public required string Country { get; set; }

//     public required string Code { get; set; }

//     public long CO2Emissions { get; set; }

//     public float YearlyChange { get; set; }

//     public static Co2EmissionsYearlyChangeSubset MapToSubset(Co2Emissions emissions)
//     {
//         return new Co2EmissionsYearlyChangeSubset
//         {
//             Country = emissions.Country,
//             Code = emissions.Code,
//             CO2Emissions = emissions.CO2Emissions
//         };
//     }
// }