using AutoMapper;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class OfferShareInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly IMapper _mapper;


        public OfferShareInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            IMapper mapper
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _mapper = mapper;
        }

        public GenericApiResponse InsertOfferedShares(OfferShareDto offerShareDto, string token)
        {
            try
            {
                return TryInsertingOfferedShare(offerShareDto, token);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus("Error In Inserting Offered Share");
            }
        }

        private GenericApiResponse ReturnErrorStatus(string s)
        {
            return _responseManager.ErrorResponse(
               s , 
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryInsertingOfferedShare(OfferShareDto offerShareDto, string token)
        {
            var extractedTokenValues = _tokenManager.GetNeededValuesFromToken(token);            

            var allOfferedShares = _repositoryWrapper.OfferedShareManager.GetOfferedSharesByUserLoginId(extractedTokenValues.UserLoginId);

            if (allOfferedShares.Any(x => x.IssuedDigitalShareId.Equals(offerShareDto.IssuedDigitalShareId)))
            {
                _loggerManager.LogInfo("This Digital Share already Offered");
                return ReturnErrorStatus("This Digital Share already Offered");
            }
            else
            {
                var offeredShareToInsert = _mapper.Map<OfferedShare>(offerShareDto);
                offeredShareToInsert.UserLoginId = extractedTokenValues.UserLoginId;
                _repositoryWrapper.OfferedShareManager.InsertOfferedShare(offeredShareToInsert);

                return _responseManager.SuccessResponse(
                    "Successfull",
                    StatusCodes.Status200OK,
                    1
                );
            }
        }
        
    }
}
