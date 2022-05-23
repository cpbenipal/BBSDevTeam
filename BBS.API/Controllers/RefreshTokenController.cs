using BBS.Dto;
using BBS.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefreshTokenController : ControllerBase
    {
        private readonly RefreshTokenInteractor _refreshTokenInteractor;

        public RefreshTokenController(RefreshTokenInteractor refreshTokenInteractor)
        {
            _refreshTokenInteractor = refreshTokenInteractor;
        }

        [HttpGet]
        public IActionResult Login([FromBody] RefreshTokenDto refreshTokenDto)
        {
            var response = _refreshTokenInteractor.RefreshToken(refreshTokenDto);
            return Ok(response);
        }
    }
}