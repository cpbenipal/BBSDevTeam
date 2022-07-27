using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllCategoriesController : ControllerBase
    {
        private readonly GetAllCategoriesInteractor _getAllCategoriesInteractor;

        public GetAllCategoriesController(GetAllCategoriesInteractor interactor)
        {
            _getAllCategoriesInteractor = interactor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllBidShares([FromQuery] int? offerShareMainTypeId)
        {
            return Ok(_getAllCategoriesInteractor.GetAllCategories(offerShareMainTypeId)); 
        }
    }
}
