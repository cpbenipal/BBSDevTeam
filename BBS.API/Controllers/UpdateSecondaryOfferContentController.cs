using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateSecondaryOfferContentController : ControllerBase
    {
        private readonly AddSecondaryOfferContentInteractor 
            _addSecondaryOfferContentInteractor;

        public UpdateSecondaryOfferContentController(AddSecondaryOfferContentInteractor interactor)
        {
            _addSecondaryOfferContentInteractor = interactor;
        }

        [Authorize]
        [HttpPost] 
        public IActionResult AddCategory(
            AddSecondaryOfferContent addCategoryContentDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _addSecondaryOfferContentInteractor
                .AddSecondaryOfferContent(token, addCategoryContentDto);

            return Ok(response);
        }
    }
}
