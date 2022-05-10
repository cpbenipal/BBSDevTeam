using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NationalitiesController : ControllerBase
    {
        private readonly GetAllNationalitiesInteractor _getAllNationalitiesInteractor;

        public NationalitiesController(GetAllNationalitiesInteractor interactor)
        {
            _getAllNationalitiesInteractor = interactor;
        }

        [HttpGet]
        public IActionResult GetAllNationalities()
        {
            return Ok(_getAllNationalitiesInteractor.GetAllNationalities());
        }
    }
}
