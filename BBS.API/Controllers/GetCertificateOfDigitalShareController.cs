using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetCertificateOfDigitalShareController : ControllerBase
    {
        private readonly GetDigitalCertificateOfIssuedShareInteractor _issueDigitalShareInteractor;

        public GetCertificateOfDigitalShareController(GetDigitalCertificateOfIssuedShareInteractor interactor)
        {
            _issueDigitalShareInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult IssueDigitalShare([FromQuery] int? digitallyIssuedShareId)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _issueDigitalShareInteractor.GetCertificateUrl(digitallyIssuedShareId, token);
            return Ok(response);
        }
    }
}
