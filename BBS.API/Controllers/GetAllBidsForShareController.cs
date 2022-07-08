using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllBidsForShareController : ControllerBase
    {
        private readonly GetAllBidsForShareInteractor _getAllBidsForShare;

        public GetAllBidsForShareController(
            GetAllBidsForShareInteractor interactor
        )
        {
            _getAllBidsForShare = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult GetAllBidsForSpecificShare(
            [FromBody] GetAllBidsForShareDto getAllBidsForShare
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _getAllBidsForShare.GetAllBidsForSpecificShare(
                token, getAllBidsForShare.ShareId
            );
            return Ok(response);
        }

    }
}
