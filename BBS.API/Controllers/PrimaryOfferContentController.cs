using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrimaryOfferContentController : ControllerBase
    {
        private readonly AddPrimaryOfferContentInteractor
            _addPrimaryOfferContentInteractor;
        private readonly UpdatePrimaryOfferContentInteractor
            _updatePrimaryOfferContentInteractor;

        public PrimaryOfferContentController(
            AddPrimaryOfferContentInteractor addPrimaryOfferContentInteractor,
            UpdatePrimaryOfferContentInteractor updatePrimaryOfferContentInteractor
        )
        {
            _addPrimaryOfferContentInteractor = addPrimaryOfferContentInteractor;
            _updatePrimaryOfferContentInteractor = updatePrimaryOfferContentInteractor;
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddCategory(
            AddPrimaryOfferContent addCategoryContentDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _addPrimaryOfferContentInteractor
                .AddPrimaryOfferContent(token, addCategoryContentDto);

            return Ok(response);
        }

        [Authorize]
        [HttpPut("Update")]
        public IActionResult UpdateCategory(
            AddPrimaryOfferContent addCategoryContentDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _updatePrimaryOfferContentInteractor
                .UpdatePrimaryOfferContent(token, addCategoryContentDto);

            return Ok(response);
        }
    }
}
