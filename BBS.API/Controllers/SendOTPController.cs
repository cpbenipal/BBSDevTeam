using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendOtpController : ControllerBase
    {
        private readonly SendOtpInteractor _sendOtpInteractor;

<<<<<<< HEAD
        public SendOtpController(SendOTPInteractor sendOtpInteractor) 
=======
        public SendOtpController(SendOtpInteractor sendOtpInteractor)
>>>>>>> RefactoredAPI7
        {
            _sendOtpInteractor = sendOtpInteractor;
        }

        [HttpPost("SendOtp")]
        public IActionResult SendOtp([FromBody] LoginUserOtpDto loginUserDto)
        {
            return Ok(_sendOtpInteractor.SendOtp(loginUserDto));
        }

        [HttpPost("VerifyOtp")]
        public IActionResult VerifyOtp([FromBody] VerifyOtpDto loginUserDto)
        {
            return Ok(_sendOtpInteractor.VerifyOtp(loginUserDto));
        }
    }
}
