using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetOfferLimitValuesController : ControllerBase
    {
        private readonly GetOfferLimitValuesInteractor _getOfferLimitValuesInteractor;

        public GetOfferLimitValuesController(GetOfferLimitValuesInteractor interactor)
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