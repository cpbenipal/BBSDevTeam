using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddCategoryController : ControllerBase
    {
        private readonly AddCategoryInteractor _addCategoryInteractor;

        public AddCategoryController(AddCategoryInteractor interactor)
        {
            _addCategoryInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddCategory(
            AddCategoryDto addCategoryContentDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _addCategoryInteractor
                .AddCategory(token, addCategoryContentDto);

            return Ok(response);
        }
    }
}
