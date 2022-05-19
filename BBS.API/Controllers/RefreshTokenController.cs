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
        public IActionResult Login([FromQuery] string accessToken, string refreshToken)
        {
            var response = _refreshTokenInteractor.RefreshToken(accessToken, refreshToken);
            return Ok(response);
        }
    }
}