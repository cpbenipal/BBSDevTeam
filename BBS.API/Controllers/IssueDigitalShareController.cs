using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueDigitalShareController : ControllerBase
    {
        public IssueDigitalSharesInteractor _issueDigitalShareInteractor { get; set; }

        public IssueDigitalShareController(IssueDigitalSharesInteractor interactor)
        {
            _issueDigitalShareInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult IssueDigitalShare([FromForm] IssueDigitalShareDto share)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _issueDigitalShareInteractor.IssueShareDigitally(share,token);
            return Ok(response);
        }
    }
}
