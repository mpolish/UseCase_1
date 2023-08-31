using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UseCase_1.Options;

namespace UseCase_1.Controllers;

/// <summary>
///     Countries controller
/// </summary>
[ApiController]
[Route("[controller]")]
public sealed class CountriesController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly CountriesOptions _countriesOptions;

    public CountriesController(IOptions<CountriesOptions> countriesOptions, IHttpClientFactory httpClientFactory)
    {
        _countriesOptions = countriesOptions.Value;
        _httpClient = httpClientFactory.CreateClient(_countriesOptions!.ApiName);
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
        try
        {
            var response = await _httpClient.GetAsync("all");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return Ok(data);
            }

            return BadRequest();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}