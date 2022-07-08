using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllOfferTimeLimitsController : ControllerBase
    {
        private readonly GetOfferTimeLimitsInteractor _getOfferLimitValuesInteractor;

        public GetAllOfferTimeLimitsController(GetOfferTimeLimitsInteractor interactor)
        {
            _getOfferLimitValuesInteractor = interactor;
        }

        [HttpGet]
        public IActionResult GetOfferLimitValues()
        {
            return Ok(_getOfferLimitValuesInteractor.Execute());
        }

    }
}