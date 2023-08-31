namespace UseCase_1;

public static class CountriesExtensions
{
    public static List<Country> FilterByCountry(this List<Country> source, string filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
        {
            return source;
        }

        return source
            .Where(x => x.Name.Common.Contains(filter, StringComparison.InvariantCultureIgnoreCase))
            .ToList();;
    }

    public static List<Country> FilterByPopulation(this List<Country> source, int filter)
    {
        if (filter <= 0)
        {
            return source;
        }

        return source.Where(x => x.Population/1_000_000 < filter)
            .ToList();;
    }

    public static List<Country> OrderCountriesBy(this List<Country> source, string direction)
    {
        if (string.IsNullOrWhiteSpace(direction))
        {
            return source;
        }

        switch (direction.ToLower())
        {
            case "ascend" :
                return source
                    .OrderBy(x => x.Name.Common)
                    .ToList();

            case "descend" :
                return source
                    .OrderByDescending(x => x.Name.Common)
                    .ToList();

            default:
                return source;
        }
    }

    public static List<Country> TakeCount(this List<Country> source, int countriesCount)
    {
        return countriesCount > 0 ?
            source
                .Take(countriesCount)
                .ToList()
            : source;
    }
}