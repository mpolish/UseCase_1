using System.Text.Json;
using FakeItEasy;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using UseCase_1.Models;
using UseCase_1.Options;
using UseCase_1.Services;

namespace UseCase_1;

[TestFixture]
public class GetCountriesTests
{

    private HttpClient _httpClient;
    private Fake<IHttpClientFactory> _httpClientFactoryFake;
    private Fake<ILogger<CountryService>> _loggerFake;
    private Fake<IOptions<CountriesOptions>> _fakeOptions;

    private int _expectedCount;
    private string _expectedCountryFilter = string.Empty;
    private string _expectedSortingDirection = string.Empty;
    private int _expectedPopulationFilter;

    [OneTimeSetUp]
    public void Setup()
    {
        _loggerFake = new Fake<ILogger<CountryService>>();
        _httpClientFactoryFake = new Fake<IHttpClientFactory>();
        _fakeOptions = new Fake<IOptions<CountriesOptions>>();
        SetupHttpClient();
    }

    [TearDown]
    public void TearDown()
    {
        _expectedCount = default;
        _expectedCountryFilter = string.Empty;
        _expectedSortingDirection = string.Empty;
        _expectedPopulationFilter = default;
    }

    [Test]
    public async Task GetCountries_FilterByName_ReturnsFilteredCountries()
    {
        // Arrange
        _expectedCountryFilter = "uKr";

        // Act
        var result = await BuildSut()
            .GetCountries(_expectedCountryFilter, _expectedSortingDirection, _expectedPopulationFilter, _expectedCount);

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Ukraine", result.Data.First().Name);
    }

    [Test]
    public async Task GetCountries_FilterByPopulation_ReturnsFilteredCountries()
    {
        // Arrange
        _expectedPopulationFilter = 31;

        // Act
        var result = await BuildSut()
            .GetCountries(_expectedCountryFilter, _expectedSortingDirection, _expectedPopulationFilter, _expectedCount);

        // Assert
        Assert.AreEqual(4, result.Count);
        Assert.Less(result.Data.First().Population/1_000_000, _expectedPopulationFilter);
    }

    [Test]
    public async Task GetCountries_SortByDescending_ReturnsSortedCountries()
    {
        // Arrange
        _expectedSortingDirection = "descend";

        // Act
        var result = await BuildSut()
            .GetCountries(_expectedCountryFilter, _expectedSortingDirection, _expectedPopulationFilter, _expectedCount);

        // Assert
        Assert.AreEqual("Ukraine", result.Data.First().Name);
        Assert.AreEqual("Fiji", result.Data.Last().Name);
    }

    [Test]
    public async Task GetCountries_SortByAscending_ReturnsSortedCountries()
    {
        // Arrange
        _expectedSortingDirection = "ascend";

        // Act
        var result = await BuildSut()
            .GetCountries(_expectedCountryFilter, _expectedSortingDirection, _expectedPopulationFilter, _expectedCount);

        // Assert
        Assert.AreEqual("Fiji", result.Data.First().Name);
        Assert.AreEqual("Ukraine", result.Data.Last().Name);
    }

    [Test]
    public async Task GetCountries_Paginate_ReturnsPaginatedCountries()
    {
        // Arrange
        _expectedCount = 1;

        // Act
        var result = await BuildSut()
            .GetCountries(_expectedCountryFilter, _expectedSortingDirection, _expectedPopulationFilter, _expectedCount);

        // Assert
        Assert.AreEqual(1, result.Count);
    }

    [Test]
    public async Task GetCountries_FilterSortAndPaginate_ReturnsFilteredSortedAndPaginatedCountries()
    {
        // Arrange
        _expectedCount = 2;
        _expectedCountryFilter = "fI";
        _expectedSortingDirection = "descend";
        _expectedPopulationFilter = 6;

        // Act
        var result = await BuildSut()
            .GetCountries(_expectedCountryFilter, _expectedSortingDirection, _expectedPopulationFilter, _expectedCount);

        // Assert
        Assert.AreEqual(_expectedCount, result.Count);
        Assert.IsTrue(result.Data.All(x => x.Name.Contains(_expectedCountryFilter, StringComparison.InvariantCultureIgnoreCase)));
        Assert.IsTrue(result.Data.All(x => x.Population/1_000_000 < _expectedPopulationFilter));
        Assert.AreEqual("Finland", result.Data.First().Name);
        Assert.AreEqual("Fiji", result.Data.Last().Name);
    }

    private static HttpClient CreateFakeHttpClient(List<Country> countryData)
    {
        var fakeMessageHandler = new FakeHttpMessageHandler(countryData);
        var httpClient = new HttpClient(fakeMessageHandler);
        httpClient.BaseAddress = new Uri("https://test.com/");
        return httpClient;
    }

    private void SetupHttpClient()
    {
        _httpClient = CreateFakeHttpClient(new List<Country>
        {
            new()
            {
                Name = new Name
                {
                    Common = "Ukraine",
                },
                Population = 35_000_000,
            },
            new()
            {
                Name = new Name
                {
                    Common = "Netherlands",
                },
                Population = 30_000_000,
            },
            new()
            {
                Name = new Name
                {
                    Common = "Finland",
                },
                Population = 5_530_719,
            },
            new()
            {
                Name = new Name
                {
                    Common = "Fiji",
                },
                Population = 896444,
            },
            new()
            {
                Name = new Name
                {
                    Common = "Ireland",
                },
                Population = 4_994_724,
            },
        });

        A.CallTo(() => _httpClientFactoryFake.FakedObject.CreateClient(A<string>._))
            .Returns(_httpClient);
    }

    private CountryService BuildSut() =>
        new(_httpClientFactoryFake.FakedObject, _fakeOptions.FakedObject, _loggerFake.FakedObject);
}

// A custom HttpMessageHandler for mocking HttpClient responses
public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly List<Country> _countryData;

    public FakeHttpMessageHandler(List<Country> countryData)
    {
        _countryData = countryData;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(_countryData))
        };
        return Task.FromResult(response);
    }
}

