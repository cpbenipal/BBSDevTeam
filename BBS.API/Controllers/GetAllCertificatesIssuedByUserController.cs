using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllCertificatesIssuedByUserController : ControllerBase
    {
        private readonly GetAllCertificatesIssuedByUserInteractor _getCertificateByUserInteractor;

        public GetAllCertificatesIssuedByUserController(GetAllCertificatesIssuedByUserInteractor interactor)
        {
            _getCertificateByUserInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult IssueDigitalShare([FromQuery] int? personId)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _getCertificateByUserInteractor.GetAllCertificatesForUser(personId,token);
            return Ok(response);
        }
    }
}
