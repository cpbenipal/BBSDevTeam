using BBS.Dto;

namespace BBS.Services.Contracts
{
    public interface IApiResponseManager
    {
        GenericApiResponse SuccessResponse(string message, int statusCode, Object returnData);
        GenericApiResponse ErrorResponse(string message, int statusCode);
    }
}
