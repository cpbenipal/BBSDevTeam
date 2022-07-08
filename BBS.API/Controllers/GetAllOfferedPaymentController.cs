using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllOfferedPaymentController : ControllerBase
    {
        private readonly GetAllOfferPaymentsInteractor _getAllOfferedPaymentInteractor;

        public GetAllOfferedPaymentController(
            GetAllOfferPaymentsInteractor getAllOfferedPaymentInteractor
        )
        {
            _getAllOfferedPaymentInteractor = getAllOfferedPaymentInteractor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllOfferedPayment()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _getAllOfferedPaymentInteractor.GetAllOfferPayments(token);
            return Ok(response);
        }

    }
}