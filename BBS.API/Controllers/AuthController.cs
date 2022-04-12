using AutoMapper;
using BBS.Models;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Mvc; 

namespace BBS.API.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private ITokenManager _tokenManager;

        public AuthController(
            IRepositoryWrapper repository,
            IMapper mapper,
            ITokenManager tokenManager
        )
        {
            _repository = repository;
            _mapper = mapper;
            _tokenManager = tokenManager;

        }

        [HttpGet]
        public IActionResult Login()
        {
            var token = _tokenManager.GenerateToken();
            return Ok(token);
        }
    }
}