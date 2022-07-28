using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetPrimaryOfferDataController : ControllerBase
    {
        private readonly GetPrimaryOfferDataInteractor _getCategoryInteractor;

        public GetPrimaryOfferDataController(GetPrimaryOfferDataInteractor interactor)
        {
            _getCategoryInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult GetCategoryContentInteractor(
            GetPrimaryOfferDataByOfferedShareDto? getCategoryDto = null
        )
        {
            var response = _getCategoryInteractor
                .GetPrimaryOfferData(getCategoryDto?.CompanyId);
            return Ok(response);
        }
    }
}
