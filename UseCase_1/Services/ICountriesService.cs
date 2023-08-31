using UseCase_1.Models;

namespace UseCase_1.Services;

public interface ICountriesService
{
    public Task<CountriesResponsePage> GetCountries(string countryName, string sortingDirection, int population,
        int countriesCount);
}