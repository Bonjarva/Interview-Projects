namespace Veyt_Project;

public class Co2EmissionsYearlyChange

{
    public required string Country { get; set; }

    public required string Code { get; set; }

    public long CO2Emissions { get; set; }

    public float YearlyChange { get; set; }
}