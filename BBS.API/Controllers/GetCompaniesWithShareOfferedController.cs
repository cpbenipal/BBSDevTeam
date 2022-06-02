using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetCompaniesWithShareOfferedController : ControllerBase
    {
        private readonly GetCompaniesWithShareOfferedInteractor _getCompaniesWithOfferedSharesInteractor;

        public GetCompaniesWithShareOfferedController(GetCompaniesWithShareOfferedInteractor interactor)
        {
            _getCompaniesWithOfferedSharesInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult IssueDigitalShare()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = 
                _getCompaniesWithOfferedSharesInteractor.GetCompaniesWithShareOffered(token);
            return Ok(response);
        }
    }
}
