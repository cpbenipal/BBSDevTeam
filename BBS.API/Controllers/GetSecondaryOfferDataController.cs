﻿using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetSecondaryOfferDataController : ControllerBase
    {
        private readonly GetSecondaryOfferDataInteractor _getCategoryInteractor;

        public GetSecondaryOfferDataController(GetSecondaryOfferDataInteractor interactor)
        {
            _getCategoryInteractor = interactor;
        }

        [Authorize]
        [HttpPost]
        public IActionResult GetCategoryContentInteractor(
            GetSecondaryOfferDataByOfferedShareDto? getCategoryDto = null
        )
        {
            var response = _getCategoryInteractor
                .GetSecondaryOfferData(getCategoryDto?.OfferedShareId);
            return Ok(response);
        }
    }
}
