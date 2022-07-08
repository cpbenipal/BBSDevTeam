using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllOfferedSharesController : ControllerBase
    {
        private readonly GetAllOfferedSharesInteractor _getOfferedSharesInteractor;

        public GetAllOfferedSharesController(GetAllOfferedSharesInteractor interactor)
        {
            _getOfferedSharesInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult OfferShare()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _getOfferedSharesInteractor.GetAllOfferedShares(token);
            return Ok(response);
        }
    }
}
