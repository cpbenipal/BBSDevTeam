using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployementTypesController : ControllerBase
    {
        private readonly GetAllEmployementTypesInteractor _getAllEmployementTypesInteractor;

        public EmployementTypesController(GetAllEmployementTypesInteractor interactor)
        {
            _getAllEmployementTypesInteractor = interactor;
        }

        [HttpGet]
        public GenericApiResponse GetAllEmployementTypes()
        {
            return _getAllEmployementTypesInteractor.GetAllEmployementTypes();
        }
    }
}
