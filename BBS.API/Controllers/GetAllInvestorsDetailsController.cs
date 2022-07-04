using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[ApiExplorerSettings(IgnoreApi = true)]

    public class GetAllInvestorsDetailsController : ControllerBase
    {
        private readonly GetAllInvestorsDetailsInteractor _getInvestorsDetailsInteractor;

        public GetAllInvestorsDetailsController(GetAllInvestorsDetailsInteractor interactor)
        {
            _getInvestorsDetailsInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult GetAllInvestorDetails()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _getInvestorsDetailsInteractor.GetAllInvestorsDetails(token);
            return Ok(response);
        }

    }
}
