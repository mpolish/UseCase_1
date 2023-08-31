namespace UseCase_1;

public static class CountriesExtensions
{
    public static List<Country> Filter(this List<Country> source, string filter)
    {
        if (!string.IsNullOrWhiteSpace(filter))
        {
            return source
                .Where(x => x.Name.Common.Contains(filter, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }

        return source;
    }
}