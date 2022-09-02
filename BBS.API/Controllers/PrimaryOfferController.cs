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
        public IActionResult GetAll(int? companyId) 
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _CompaniesInteractor.GetListing(token,companyId);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult Add(
            PrimaryOfferDto addCategoryContentDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _addPrimaryOfferContentInteractor
                .AddPrimaryOffer(token, addCategoryContentDto);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("AddContent")]
        public IActionResult AddContent(
           PrimaryOfferingContentDto content 
       )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _addPrimaryOfferContentInteractor
                .AddPrimaryOfferContent(token, content);

            return Ok(response);
        }

        [Authorize]
        [HttpPut("UpdateContent")]
        public IActionResult UpdateContent(
           PrimaryOfferingContentDto content
       )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _updatePrimaryOfferContentInteractor
                .UpdatePrimaryOfferContent(token, content);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("DeleteContent")]
        public IActionResult DeleteContent(
        [FromBody] int contentId
      )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _addPrimaryOfferContentInteractor
                .DeleteContent(token, contentId); 

            return Ok(response);
        }

        [Authorize]
        [HttpPut("Update")]
        public IActionResult Update( 
            PrimaryOfferDto addCategoryContentDto
        )
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var response = _updatePrimaryOfferContentInteractor
                .UpdatePrimaryOffer(token, addCategoryContentDto);

            return Ok(response);
        }
    }
}
