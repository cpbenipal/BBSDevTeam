using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetCategoryController : ControllerBase
    {
        private readonly GetCategoryInteractor _getCategoryInteractor;

        public GetCategoryController(GetCategoryInteractor interactor)
        {
            _getCategoryInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult GetCategoryContentInteractor(
            GetCategoryByTypeDto? getCategoryDto = null
        )
        {
            var response = _getCategoryInteractor
                .GetCategory(getCategoryDto?.offeredShareMainTypeId);
            return Ok(response);
        }
    }
}
