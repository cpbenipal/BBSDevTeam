using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetPrivatelyOfferedShareController : ControllerBase
    {
        private readonly GetPrivatelyOfferedShareInteractor _getPrivatelyOfferedSharesInteractor;

        public GetPrivatelyOfferedShareController(GetPrivatelyOfferedShareInteractor interactor)
        {
            _getPrivatelyOfferedSharesInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Login([FromQuery] string offerPrivateKey)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _getPrivatelyOfferedSharesInteractor
                .GetPrivatelyOfferedShareByPrivateKey(token, offerPrivateKey);
            return Ok(response);
        }

    }
}