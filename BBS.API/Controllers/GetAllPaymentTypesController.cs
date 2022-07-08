using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllPaymentTypesController : ControllerBase
    {
        private readonly GetAllPaymentTypesInteractor _getAllPaymentTypesInteractor;

        public GetAllPaymentTypesController(GetAllPaymentTypesInteractor interactor)
        {
            _getAllPaymentTypesInteractor = interactor;
        }

        [HttpGet]
        public IActionResult GetOfferLimitValues()
        {
            return Ok(_getAllPaymentTypesInteractor.GetAllPaymentTypes());
        }

    }
}