using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddPrimaryOfferContentController : ControllerBase
    {
        private readonly AddPrimaryOfferContentInteractor
            _addPrimaryOfferContentInteractor;

        public AddPrimaryOfferContentController(AddPrimaryOfferContentInteractor interactor)
        {
            _addPrimaryOfferContentInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddCategory(
            AddPrimaryOfferContent addCategoryContentDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _addPrimaryOfferContentInteractor
                .AddPrimaryOfferContent(token, addCategoryContentDto);

            return Ok(response);
        }
    }
}
