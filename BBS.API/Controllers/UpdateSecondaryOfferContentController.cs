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
        private readonly UpdateSecondaryOfferContentInteractor
            _updateSecondaryOfferContentInteractor;

        public UpdateSecondaryOfferContentController(UpdateSecondaryOfferContentInteractor interactor)
        {
            _updateSecondaryOfferContentInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(
            AddSecondaryOfferContent addCategoryContentDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _updateSecondaryOfferContentInteractor
                .UpdateSecondaryOfferContent(token, addCategoryContentDto);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("DeleteContent")]
        public IActionResult DeleteContent(
      [FromBody] int contentId
    )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _updateSecondaryOfferContentInteractor
                .DeleteContent(token, contentId);

            return Ok(response);
        }


    }
}
