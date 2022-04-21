using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetRegisteredSharesController : ControllerBase
    {
        private readonly GetRegisteredSharesInteractor _getRegisteredSharesInteractor;

        public GetRegisteredSharesController(GetRegisteredSharesInteractor getRegisteredSharesInteractor)
        {
            _getRegisteredSharesInteractor = getRegisteredSharesInteractor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetRegisteredShares()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _getRegisteredSharesInteractor.GetRegisteredShares(token);
            return Ok(response);
        }
    }
}
