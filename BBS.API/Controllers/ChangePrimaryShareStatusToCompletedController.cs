using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[ApiExplorerSettings(IgnoreApi = true)]

    public class ChangePrimaryShareStatusToCompletedController : ControllerBase
    {
        private readonly ChangePrimaryShareStatusToCompletedInteractor
            _changeShareStatusToPendingInteractor;

        public ChangePrimaryShareStatusToCompletedController(
            ChangePrimaryShareStatusToCompletedInteractor interactor
        )
        {
            _changeShareStatusToPendingInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangeShareStatusToPending(
            [FromBody] ChangePrimaryShareStatusToCompletedDto changeShareStatusDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _changeShareStatusToPendingInteractor
                .ChangePrimaryShareStatusToCompleted(
                    token, changeShareStatusDto.PrimaryOfferShareId
                );
            return Ok(response);
        }

    }
}
