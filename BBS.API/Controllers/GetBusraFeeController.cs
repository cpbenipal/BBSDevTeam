using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetBusraFeeController : ControllerBase
    {
        private readonly GetBusraFeeInteractor _getBusraFeeInteractor;

        public GetBusraFeeController(GetBusraFeeInteractor getBusraFeeInteractor)
        {
            _getBusraFeeInteractor = getBusraFeeInteractor;
        }

        [HttpPost]
        public IActionResult GetBusraFee()
        {
            return Ok(_getBusraFeeInteractor.GetBusraFee());
        }

    }
}