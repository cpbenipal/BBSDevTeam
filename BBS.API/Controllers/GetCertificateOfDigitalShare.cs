using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetCertificateOfDigitalShare : ControllerBase
    {
        private readonly GetDigitalCertificateOfIssuedShareInteractor _issueDigitalShareInteractor;

        public GetCertificateOfDigitalShare(GetDigitalCertificateOfIssuedShareInteractor interactor)
        {
            _issueDigitalShareInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult IssueDigitalShare([FromQuery] int digitallyIssuedShareId)
        {
            var response = _issueDigitalShareInteractor.GetCertificateUrl(digitallyIssuedShareId);
            return Ok(response);
        }
    }
}
