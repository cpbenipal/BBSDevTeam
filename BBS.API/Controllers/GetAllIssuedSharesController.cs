using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllIssuedSharesController : ControllerBase
    {
        private readonly GetAllIssuedSharesInteractor _issueDigitalShareInteractor;

        public GetAllIssuedSharesController(GetAllIssuedSharesInteractor interactor)
        {
            _issueDigitalShareInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult IssueDigitalShare()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _issueDigitalShareInteractor.GetAllIssuedShares(token);
            return Ok(response);
        }
    }
}
