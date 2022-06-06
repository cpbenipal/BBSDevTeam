using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthInteractor _loginUserInteractor;

        public AuthController(AuthInteractor loginUserInteractor)
        {
            _loginUserInteractor = loginUserInteractor;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            return Ok(_loginUserInteractor.LoginUser(loginUserDto));
        }

        [HttpGet("CheckEmailOrPhone")]
        public IActionResult CheckEmailOrPhone(string emailOrPhone)
        {
            return Ok(_loginUserInteractor.CheckEmailOrPhone(emailOrPhone));
        }

        [HttpGet("CheckEmiratesId")]
        public IActionResult CheckEmiratesId(string emiratesId)
        {
            return Ok(_loginUserInteractor.CheckEmiratesId(emiratesId));
        }
    }
}