using BBS.Dto;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class ApiResponseManager : IApiResponseManager
    {
        private readonly GenericApiResponse response;
        public ApiResponseManager()
        {
            response = new GenericApiResponse();
        }

        public GenericApiResponse ErrorResponse(string message, int statusCode)
        {
            response.ReturnCode = statusCode;
            response.ReturnMessage = message;
            response.ReturnData = "";
            response.ReturnStatus = false;

            return response;
        }

        public GenericApiResponse SuccessResponse(string message, int statusCode, object returnData)
        {
            response.ReturnCode = statusCode;
            response.ReturnMessage = message;
            response.ReturnData = returnData;
            response.ReturnStatus = true;
            return response;
        }
    }
}
