using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddCategoryContentController : ControllerBase
    {
        private readonly AddCategoryContentInteractor _addCategoryContentInteractor;

        public AddCategoryContentController(AddCategoryContentInteractor interactor)
        {
            _addCategoryContentInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult GetCategoryContentInteractor(
            AddCategoryContentDto addCategoryContentDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _addCategoryContentInteractor
                .AddCategoryContent(token, addCategoryContentDto);

            return Ok(response);
        }
    }
}
