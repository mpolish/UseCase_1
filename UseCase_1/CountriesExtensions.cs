namespace UseCase_1;

public static class CountriesExtensions
{
    public static List<Country> FilterByCountry(this List<Country> source, string filter)
    {
        if (!string.IsNullOrWhiteSpace(filter))
        {
            return source
                .Where(x => x.Name.Common.Contains(filter, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }

        return source;
    }

    public static List<Country> FilterByPopulation(this List<Country> source, int filter)
    {
        if (filter > 0)
        {
            return source
                .Where(x => x.Population/1_000_000 < filter)
                .ToList();
        }

        return source;
    }
}