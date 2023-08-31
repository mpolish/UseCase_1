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

    public static List<Country> OrderCountriesBy(this List<Country> source, string direction)
    {
        if (!string.IsNullOrWhiteSpace(direction))
        {
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
            }
        }

        return source;
    }
}