using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidShareController : ControllerBase
    {
        private readonly BidShareInteractor _bidShareInteractor;

        public BidShareController(BidShareInteractor interactor)
        {
            _bidShareInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult BidShare([FromBody] BidShareDto bidShareDto)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _bidShareInteractor.InsertBidShare(token, bidShareDto);
            return Ok(response);
        }

    }
}
