using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferPaymentController : ControllerBase
    {
        private readonly OfferPaymentInteractor _offerPaymentInteractor;

        public OfferPaymentController(OfferPaymentInteractor offerPaymentInteractor)
        {
            _offerPaymentInteractor = offerPaymentInteractor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult OfferPayment([FromBody] OfferPaymentDto offerPaymentDto)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _offerPaymentInteractor.InsertOfferPayment(token,offerPaymentDto);
            return Ok(response);
        }

    }
}