using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

public class CsvFileRetrievalService : IHostedService
{
    private readonly HttpClient _httpClient;

    // Property to store the CSV content
    public string CsvContent { get; private set; }

    public CsvFileRetrievalService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        CsvContent = string.Empty;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var csvUrl = "https://raw.githubusercontent.com/GreenfactAS/co2-dataset/1643f42aa036be7f24c4db6a9441952372dc312a/CO2-emissions.csv";
            CsvContent = await _httpClient.GetStringAsync(csvUrl);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Failed to retrieve CSV file. Error: {ex.Message}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}