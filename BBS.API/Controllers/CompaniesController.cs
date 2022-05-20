using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly GetAllCompaniesInteractor _getAllCompaniesInteractor;

        public CompaniesController(GetAllCompaniesInteractor interactor)
        {
            _getAllCompaniesInteractor = interactor;
        }

        [HttpGet]
        public IActionResult GetAllCompanies([FromQuery] string? keyword)
        {
            return Ok(_getAllCompaniesInteractor.GetAllCompanies(keyword));
        }
    }
}
