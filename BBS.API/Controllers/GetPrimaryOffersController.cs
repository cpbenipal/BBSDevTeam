using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetPrimaryOffersController : ControllerBase
    {
        private readonly GetPrimaryOfferDataInteractor _getCategoryInteractor;

        public GetPrimaryOffersController(GetPrimaryOfferDataInteractor interactor)
        { 
            _getCategoryInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetCategoryContentInteractor()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _getCategoryInteractor
                .GetPrimaryOffers(token);
            return Ok(response);
        }
    }
}
