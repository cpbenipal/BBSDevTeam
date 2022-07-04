using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            GetCategoryContentDto? getCategoryContentDto = null
        )
        {
            var response = _getCategoryContentInteractor
                .GetCategoryContent(getCategoryContentDto?.categoryId);
            return Ok(response);
        }
    }
}
