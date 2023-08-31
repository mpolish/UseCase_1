using UseCase_1.Models;

namespace UseCase_1;

public sealed class Mapper
{
    public CountriesResponsePage MapToCountriesResponse(List<Country> countries)
    {
        return new CountriesResponsePage
        {
            Count = countries.Count,
            Data = countries.Select(MapToCountryResponse).ToArray(),
        };
    }

    private CountryResponse MapToCountryResponse(Country country)
    {
        return new CountryResponse
        {
            Name = country.Name.Common,
            Population = country.Population,
        };
    }
}