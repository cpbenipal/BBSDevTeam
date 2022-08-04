using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllPrimaryOfferInvestorsController : ControllerBase
    {
        private readonly GetPrimaryOfferDataInteractor _getCategoryInteractor;

        public GetAllPrimaryOfferInvestorsController(GetPrimaryOfferDataInteractor interactor)
        {
            _getCategoryInteractor = interactor;
        }        

        [Authorize]
        [HttpPost]
        public IActionResult GetCategoryContentInteractor([FromBody] CompanyDto Company)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _getCategoryInteractor.GetPrimaryOffers(token, Company.CompanyId);
            return Ok(response);
        } 
    }
}
