using Microsoft.Extensions.Options;
using UseCase_1.Options;

namespace UseCase_1;

public class CountriesService : ICountriesService
{
    private readonly ILogger<CountriesService> _logger;
    private readonly HttpClient _httpClient;

    public CountriesService(IHttpClientFactory httpClientFactory, IOptions<CountriesOptions> countriesOptions, ILogger<CountriesService> logger)
    {
        var options = countriesOptions.Value;
        _httpClient = httpClientFactory.CreateClient(options!.ApiName);
        _logger = logger;
    }

    public async Task<List<Country>> GetCountries(string countryName, string sortingDirection, int population, int countriesCount)
    {
        try
        {
            var response = await _httpClient.GetAsync("all");

            if (response.IsSuccessStatusCode)
            {
                var countries = await response.Content.ReadFromJsonAsync<List<Country>>();

                return countries?
                    .FilterByName(countryName)
                    .FilterByPopulation(population)
                    .OrderByDirection(sortingDirection)
                    .TakeCount(countriesCount) ?? Enumerable.Empty<Country>().ToList();
            }

            return Enumerable.Empty<Country>().ToList();;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong during external API request");
            throw;
        }
    }
}