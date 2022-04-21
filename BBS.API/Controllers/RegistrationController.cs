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
            try
            {
                var response = _registerUserInteractor.RegisterUser(registerUserDto);
                return Ok(response);
            }
            catch (Exception e)
            {
                return ErrorResponse(e.Message);
            }
        }
       
        private IActionResult ErrorResponse(string errorMessage)
        {
            var authResult = new GenericApiResponse();
            authResult.ReturnCode = 0;
            authResult.ReturnMessage = errorMessage;
            authResult.ReturnData = null;
            authResult.ReturnStatus = false;

            return StatusCode(500, authResult);
        }
    }
}
