using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChangeUserStatusToCompletedController : ControllerBase
    {
        private readonly ChangeUserStatusToCompletedInteractor 
            _changeUserStatusToPendingInteractor;

        public ChangeUserStatusToCompletedController(
            ChangeUserStatusToCompletedInteractor interactor
        )
        {
            _changeUserStatusToPendingInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangeUserStatusToPending(
            [FromBody] ChangeUserStatusToCompletedDto changeUserStatusDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _changeUserStatusToPendingInteractor
                .ChangeUserStatusToCompleted(token, changeUserStatusDto.PersonId);
            return Ok(response);
        }

    }
}
