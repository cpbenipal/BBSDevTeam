using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly RegisterUserInteractor _registerUserInteractor; 

        public RegistrationController(RegisterUserInteractor registerUserInteractor)
        {
            _registerUserInteractor = registerUserInteractor;
        }

        [HttpPost] 
        public IActionResult Register([FromForm] RegisterUserDto registerUserDto)
        {
            var response = _registerUserInteractor.RegisterUser(registerUserDto);
            return Ok(response);
        }
    }
}
