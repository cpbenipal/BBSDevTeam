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

        [HttpPost]
        public IActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            return Ok(_loginUserInteractor.LoginUser(loginUserDto));
        }

        [HttpPost("CheckEmailOrPhone")]
        public IActionResult CheckEmailOrPhone([FromBody] CheckEmailOrPhoneDto checkEmailOrPhoneDto)
        {
            return Ok(_loginUserInteractor.CheckEmailOrPhone(checkEmailOrPhoneDto.EmailOrPhone));
        }

        [HttpPost("CheckEmiratesId")]
        public IActionResult CheckEmiratesId([FromBody] CheckEmiratesIdDto checkEmiratesId)
        {
            return Ok(_loginUserInteractor.CheckEmiratesId(checkEmiratesId.EmiratesId));
        }
    }
}