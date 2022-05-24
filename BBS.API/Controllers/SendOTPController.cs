using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendOTPController : ControllerBase
    {
        private readonly SendOTPInteractor _sendOtpInteractor;

        public SendOTPController(SendOTPInteractor sendOtpInteractor)
        {
            _sendOtpInteractor = sendOtpInteractor;
        }

        [HttpPost("SendOTP")]
        public IActionResult SendOTP([FromBody] LoginUserOTPDto loginUserDto)
        {
            return Ok(_sendOtpInteractor.SendOTP(loginUserDto));
        }

        [HttpPost("VerifyOTP")]
        public IActionResult VerifyOTP([FromBody] VerifyOTPDto loginUserDto)
        {
            return Ok(_sendOtpInteractor.VerifyOTP(loginUserDto));
        }
    }
}
