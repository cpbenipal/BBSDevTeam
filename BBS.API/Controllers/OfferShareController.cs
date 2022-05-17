using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferShareController : ControllerBase
    {
        private readonly OfferShareInteractor _offerShareInteractor;

        public OfferShareController(OfferShareInteractor interactor)
        {
            _offerShareInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult OfferShare([FromBody] OfferShareDto share)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _offerShareInteractor.InsertOfferedShares(share, token);
            return Ok(response);
        }
    }
}
