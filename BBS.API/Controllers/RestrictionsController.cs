using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestrictionsController : ControllerBase
    {
        private readonly GetAllRestrictionsInteractor _getAllRestrictionsInteractor;

        public RestrictionsController(GetAllRestrictionsInteractor interactor)
        {
            _getAllRestrictionsInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllRestrictions()
        {
            return Ok(_getAllRestrictionsInteractor.GetAllRestrictions());
        }
    }
}
