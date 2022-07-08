using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllShareDetailsController : ControllerBase
    {
        private readonly GetAllShareDetailsInteractor _getShareDetailsInteractor;

        public GetAllShareDetailsController(GetAllShareDetailsInteractor interactor)
        {
            _getShareDetailsInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllShareDetails()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _getShareDetailsInteractor.GetAllShareDetails(token);
            return Ok(response);
        }

    }
}
