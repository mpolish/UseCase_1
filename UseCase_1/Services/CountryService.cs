using Microsoft.Extensions.Options;
using UseCase_1.Models;
using UseCase_1.Options;

namespace UseCase_1.Services;

public class CountryService : ICountriesService
{
    private readonly Mapper _mapper = new();
    private readonly ILogger<CountryService> _logger;
    private readonly HttpClient _httpClient;

    public CountryService(IHttpClientFactory httpClientFactory, IOptions<CountriesOptions> countriesOptions, ILogger<CountryService> logger)
    {
        var options = countriesOptions.Value;
        _httpClient = httpClientFactory.CreateClient(options!.ApiName);
        _logger = logger;
    }

    public async Task<CountriesResponsePage> GetCountries(string countryName, string sortingDirection, int population,
        int countriesCount)
    {
        try
        {
            var response = await _httpClient.GetAsync("all");

            if (response.IsSuccessStatusCode)
            {
                var countries = await response.Content.ReadFromJsonAsync<List<Country>>() ??
                                Enumerable.Empty<Country>().ToList();

                var withAppliedParameters = countries
                    .FilterByName(countryName)
                    .FilterByPopulation(population)
                    .OrderByDirection(sortingDirection)
                    .TakeCount(countriesCount)
                    .ToList();

                return _mapper.MapToCountriesResponse(withAppliedParameters);
            }

            return new CountriesResponsePage();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong during external API request");
            throw;
        }
    }
}