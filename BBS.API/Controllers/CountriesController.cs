using BBS.Dto;
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
        public GenericApiResponse GetAllCountries()
        {
            return _getAllCountriesInteractor.GetAllCountries();
        }
    }
}
