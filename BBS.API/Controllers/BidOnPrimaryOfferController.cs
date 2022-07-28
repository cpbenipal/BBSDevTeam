using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidOnPrimaryOfferController : ControllerBase
    {
        private readonly BidOnPrimaryOfferInteractor _bidOnPrimaryInteractor;

        public BidOnPrimaryOfferController(BidOnPrimaryOfferInteractor interactor)
        {
            _bidOnPrimaryInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult BidOnPrimaryOffer([FromBody] BidOnPrimaryOfferingDto bidShareDto)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _bidOnPrimaryInteractor.BidOnPrimaryOffers(bidShareDto, token);
            return Ok(response);
        }

    }
}
