using Microsoft.AspNetCore.Mvc;

namespace UseCase_1.Controllers;

/// <summary>
///     Countries controller
/// </summary>
[ApiController]
[Route("[controller]")]
public sealed class CountriesController : ControllerBase
{
    private readonly ICountriesService _countriesService;
    public CountriesController(ICountriesService countriesService)
    {
        _countriesService = countriesService;
    }

    /// <summary>
    ///     Retrieve countries.
    /// </summary>
    /// <param name="population">Population filter</param>
    /// <param name="countriesCount">Countries count</param>
    /// <param name="countryName">Country name filter</param>
    /// <param name="sortingDirection">Sorting direction</param>
    /// <returns>
    ///     Returns filtered, sorted and paged countries list
    /// </returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCountries(
        [FromQuery] int population,
        [FromQuery] int countriesCount,
        [FromQuery] string countryName = "",
        [FromQuery] string sortingDirection = "")
    {
        var response = await _countriesService.GetCountries(countryName, sortingDirection, population, countriesCount);

        if (response.Any())
        {
            return Ok(response);
        }

        return BadRequest();
    }
}