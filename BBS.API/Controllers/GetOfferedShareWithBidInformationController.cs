using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetOfferedShareWithBidInformationController : ControllerBase
    {
        private readonly GetOfferedShareWithBidInformationInteractor 
            _getOfferedShareWithBidInfoInteractor;

        public GetOfferedShareWithBidInformationController(
            GetOfferedShareWithBidInformationInteractor interactor
        )
        {
            _getOfferedShareWithBidInfoInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult OfferShare()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = 
                _getOfferedShareWithBidInfoInteractor
                .GetOfferedShareWithBidInformation(token);
            return Ok(response);
        }
    }
}
