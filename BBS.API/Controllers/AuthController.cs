using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUserInteractor _loginUserInteractor;

        public AuthController(LoginUserInteractor loginUserInteractor)
        {
            _loginUserInteractor = loginUserInteractor;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            return Ok(_loginUserInteractor.LoginUser(loginUserDto));
        }
         
    }
}