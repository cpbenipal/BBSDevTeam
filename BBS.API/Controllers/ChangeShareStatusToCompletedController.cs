using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChangeShareStatusToCompletedController : ControllerBase
    {
        private readonly ChangeShareStatusToCompletedInteractor
            _changeShareStatusToPendingInteractor;

        public ChangeShareStatusToCompletedController(
            ChangeShareStatusToCompletedInteractor interactor
        )
        {
            _changeShareStatusToPendingInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangeShareStatusToPending(
            [FromBody] ChangeShareStatusToCompletedDto changeShareStatusDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _changeShareStatusToPendingInteractor
                .ChangeShareStatusToCompleted(token, changeShareStatusDto.ShareId);
            return Ok(response);
        }

    }
}
