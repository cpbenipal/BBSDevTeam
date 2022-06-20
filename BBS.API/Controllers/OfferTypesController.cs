using BBS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferTypesController : ControllerBase
    { 
        [Authorize]
        [HttpGet]
        public IActionResult GetOfferType()
        {
            var types = new List<OfferType>()
            {
                 new OfferType { Id = 1, Name = "Auction" },
                  new OfferType { Id = 2, Name = "Private" }
            };

            return Ok(types);
        }
    }
}
