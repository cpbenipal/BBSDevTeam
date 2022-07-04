using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetCategoryContentController : ControllerBase
    {
        private readonly GetCategoryContentInteractor _getCategoryContentInteractor;

        public GetCategoryContentController(GetCategoryContentInteractor interactor)
        {
            _getCategoryContentInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult GetCategoryContentInteractor(
            GetCategoryContentDto getCategoryContentDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _getCategoryContentInteractor
                .GetCategoryContent(token, getCategoryContentDto.categoryId);
            return Ok(response);
        }
    }
}
