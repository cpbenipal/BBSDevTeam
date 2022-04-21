using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetProfileInformationController : ControllerBase
    {
        private readonly GetProfileInformationInteractor _getProfileInformationInteractor;

        public GetProfileInformationController(GetProfileInformationInteractor interactor)
        {
            _getProfileInformationInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetProfileInformation()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _getProfileInformationInteractor.GetUserProfileInformation(token);
            return Ok(response);
        }
    }
}
