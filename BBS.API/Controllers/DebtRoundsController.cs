using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebtRoundsController : ControllerBase
    {
        private readonly GetAllDebtRoundsInteractor _getAllDebtRoundsInteractor;

        public DebtRoundsController(GetAllDebtRoundsInteractor interactor)
        {
            _getAllDebtRoundsInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public GenericApiResponse GetAllDebtRounds()
        {
            return _getAllDebtRoundsInteractor.GetAllDebtRounds();
        }
    }
}
