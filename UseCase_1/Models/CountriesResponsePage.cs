namespace UseCase_1.Models;

public sealed class CountriesResponsePage
{
    public int Count { get; set; }
    public CountryResponse[] Data { get; set; }
}