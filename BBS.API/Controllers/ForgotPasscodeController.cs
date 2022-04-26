using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForgotPasscodeController : ControllerBase
    {
        private readonly ForgotPasscodeInteractor _forgotPasswordInteractor;

        public ForgotPasscodeController(ForgotPasscodeInteractor interactor)
        {
            _forgotPasswordInteractor = interactor;
        }

        [HttpPost]
        public IActionResult SendOTP([FromBody] ForgotPasscodeDto forgotPassDto)
        {
            return Ok(_forgotPasswordInteractor.ForgotPasscode(forgotPassDto));
        }
    }
}
