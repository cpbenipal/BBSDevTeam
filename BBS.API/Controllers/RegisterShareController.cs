using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterShareController : ControllerBase
    {
        private readonly RegisterShareInteractor _registerShareInteractor;

        public RegisterShareController(RegisterShareInteractor registerShareInteractor)
        {
            _registerShareInteractor = registerShareInteractor;
        }

        [HttpPost]
        public IActionResult RegisterShare([FromForm] RegisterShareDto registerShareDto)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _registerShareInteractor.RegisterShare(registerShareDto, token!);
            return Ok(response);
        }
    }
}
