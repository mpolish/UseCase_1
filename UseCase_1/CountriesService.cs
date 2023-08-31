using System.Text.Json;
using Microsoft.Extensions.Options;
using UseCase_1.Options;

namespace UseCase_1;

public class CountriesService : ICountriesService
{
    private readonly HttpClient _httpClient;

    public CountriesService(IHttpClientFactory httpClientFactory, IOptions<CountriesOptions> countriesOptions)
    {
        var options = countriesOptions.Value;
        _httpClient = httpClientFactory.CreateClient(options!.ApiName);
    }

    public async Task<List<Country>> GetCountries(string countryName, string param2, int population)
    {
        try
        {
            var response = await _httpClient.GetAsync("all");

            if (response.IsSuccessStatusCode)
            {
                var countries = await response.Content.ReadFromJsonAsync<List<Country>>();

                return countries?
                    .FilterByCountry(countryName)
                    .FilterByPopulation(population) ?? Enumerable.Empty<Country>().ToList();
            }

            return Enumerable.Empty<Country>().ToList();;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}