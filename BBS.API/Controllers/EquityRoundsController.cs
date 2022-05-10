using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquityRoundsController : ControllerBase
    {
        private readonly GetAllEquityRoundsInteractor _getAllEquityRoundsInteractor;

        public EquityRoundsController(GetAllEquityRoundsInteractor interactor)
        {
            _getAllEquityRoundsInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllEquityRounds()
        {
            return Ok(_getAllEquityRoundsInteractor.GetAllEquityRounds());
        }
    }
}
