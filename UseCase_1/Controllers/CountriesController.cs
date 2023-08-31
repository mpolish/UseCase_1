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
    /// <param name="param1">Param 1</param>
    /// <param name="param2">Param 2</param>
    /// <param name="param3">Param 3</param>
    /// <returns>
    ///     Returns countries list
    /// </returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCountries(
        [FromQuery] int param1,
        [FromQuery] string param2 = "",
        [FromQuery] string param3 = "")
    {
        var response = await _countriesService.GetCountries(param2, param3, param1);

        if (response.Any())
        {
            return Ok(response);
        }

        return BadRequest();
    }
}