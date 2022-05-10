using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageLocationsController : ControllerBase
    {
        private readonly GetAllStorageLocationsInteractor _getAllStorageLocationsInteractor;

        public StorageLocationsController(GetAllStorageLocationsInteractor interactor)
        {
            _getAllStorageLocationsInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllStorageLocation()
        {
            return Ok(_getAllStorageLocationsInteractor.GetAllStorageLocations());
        }
    }
}
