using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly GetAllCountriesInteractor _getAllCountriesInteractor;

        public CountriesController(GetAllCountriesInteractor interactor)
        {
            _getAllCountriesInteractor = interactor;
        }

        [HttpGet]
        public IActionResult GetAllCountries([FromQuery] string? keyword)
        {
            return Ok(_getAllCountriesInteractor.GetAllCountries(keyword));
        }
    }
}
