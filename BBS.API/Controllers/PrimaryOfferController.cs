using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrimaryOfferController : ControllerBase 
    {
        private readonly AddPrimaryOfferContentInteractor
            _addPrimaryOfferContentInteractor;
        private readonly UpdatePrimaryOfferContentInteractor
            _updatePrimaryOfferContentInteractor;

        private readonly GetPrimaryOfferDataInteractor _CompaniesInteractor;

        public PrimaryOfferController(
            AddPrimaryOfferContentInteractor addPrimaryOfferContentInteractor,
            UpdatePrimaryOfferContentInteractor updatePrimaryOfferContentInteractor,
            GetPrimaryOfferDataInteractor CompaniesInteractor
        )
        {
            _addPrimaryOfferContentInteractor = addPrimaryOfferContentInteractor;
            _updatePrimaryOfferContentInteractor = updatePrimaryOfferContentInteractor;
            _CompaniesInteractor = CompaniesInteractor;
        }
        /// <summary>
        /// Get All offering companies
        /// </summary>
        /// <returns>Id, CompanyName , OfferPrice , Quanity , totalBids</returns>
        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetAll() 
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _CompaniesInteractor.GetListing(token);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult Add(
            AddPrimaryOfferContent addCategoryContentDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _addPrimaryOfferContentInteractor
                .AddPrimaryOfferContent(token, addCategoryContentDto);

            return Ok(response);
        }

        [Authorize]
        [HttpPut("Update")]
        public IActionResult Update( 
            AddPrimaryOfferContent addCategoryContentDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _updatePrimaryOfferContentInteractor
                .UpdatePrimaryOfferContent(token, addCategoryContentDto);

            return Ok(response);
        }
    }
}
