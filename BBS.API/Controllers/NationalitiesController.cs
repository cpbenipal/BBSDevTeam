using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NationalitiesController : ControllerBase
    {
        private readonly GetAllNationalitiesInteractor _getAllNationalitiesInteractor;

        public NationalitiesController(GetAllNationalitiesInteractor interactor)
        {
            _getAllNationalitiesInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public GenericApiResponse GetAllNationalities()
        {
            return _getAllNationalitiesInteractor.GetAllNationalities();
        }
    }
}
