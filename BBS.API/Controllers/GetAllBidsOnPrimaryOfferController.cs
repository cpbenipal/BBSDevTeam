using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllBidsOnPrimaryOfferController : ControllerBase
    {
        private readonly GetAllBidsOnPrimaryOfferInteractor _getAllBidsOnPrimaryOfferInteractor;

        public GetAllBidsOnPrimaryOfferController(GetAllBidsOnPrimaryOfferInteractor interactor)
        {
            _getAllBidsOnPrimaryOfferInteractor = interactor;
        }

        [HttpGet]
        public IActionResult GetAllBidsOnPrimaryOffer([FromQuery] int? companyId)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            return Ok(_getAllBidsOnPrimaryOfferInteractor.GetAllBidsOnPrimaryOffer(token, companyId));
        }
    }
}
