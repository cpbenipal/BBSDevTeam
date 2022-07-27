using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllBidSharesController : ControllerBase
    {
        private readonly GetAllBidSharesInteractor _getAllBidSharesInteractor;

        public GetAllBidSharesController(GetAllBidSharesInteractor interactor)
        {
            _getAllBidSharesInteractor = interactor;
        }

        [HttpGet]
        public IActionResult GetAllBidShares()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            return Ok(_getAllBidSharesInteractor.GetAllBidShares(token));
        }
    }
}
