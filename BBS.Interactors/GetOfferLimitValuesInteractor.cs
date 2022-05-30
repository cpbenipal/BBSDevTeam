using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetOfferLimitValuesInteractor
    {
        private readonly IApiResponseManager _responseManager;
        public GetOfferLimitValuesInteractor(IApiResponseManager responseManager)
        {
            _responseManager = responseManager;
        }

        public GenericApiResponse Execute()
        {
            return _responseManager.SuccessResponse(
                 "Successfull",
                StatusCodes.Status201Created,
                new List<int> { 1, 2, 3, 4 }
            );
        }
    }
}
