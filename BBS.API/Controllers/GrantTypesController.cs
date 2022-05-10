using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrantTypesController : ControllerBase
    {
        private readonly GetAllGrantTypesInteractor _getAllGrantTypesInteractor;

        public GrantTypesController(GetAllGrantTypesInteractor interactor)
        {
            _getAllGrantTypesInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllGrantTypes()
        {
            return Ok(_getAllGrantTypesInteractor.GetAllGrantTypes());
        }
    }
}
