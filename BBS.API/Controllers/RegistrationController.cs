using BBS.Dto;
using BBS.Services.Contracts;
using Interactors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly RegisterUserInteractor _registerUserInteractor; 
        private readonly IFileUploadService _uploadService;

        public RegistrationController(RegisterUserInteractor registerUserInteractor, IFileUploadService uploadService)
        {
            _registerUserInteractor = registerUserInteractor;
            _uploadService = uploadService;
        }

        [HttpPost] 
        public IActionResult Register([FromForm] RegisterUserDto registerUserDto)
        {
            try
            {
                var response = _registerUserInteractor.RegisterUser(registerUserDto);
                return Ok(response);
            }
            catch (Exception e)
            {
                return ErrorResponse(e.Message);
            }
        }
        //[HttpPost("upload"), DisableRequestSizeLimit]
        //public IActionResult RegisterUpload([FromForm] FileModel Attachments)
        //{
        //    try
        //    { 
        //        var response =  _uploadService.UploadFileToBlob(Attachments);
        //        return Ok(response);
        //    }
        //    catch (Exception e)
        //    {
        //        return ErrorResponse(e.Message);
        //    }
        //}
        private IActionResult ErrorResponse(string errorMessage)
        {
            var authResult = new GenericApiResponse();
            authResult.ReturnCode = 0;
            authResult.ReturnMessage = errorMessage;
            authResult.ReturnData = null;
            authResult.ReturnStatus = false;

            return StatusCode(500, authResult);
        }
    }
}
