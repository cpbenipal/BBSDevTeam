﻿using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class OfferPaymentInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly OfferPaymentUtils _offerPaymentUtils;

        public OfferPaymentInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            OfferPaymentUtils offerPaymentUtils,
            ITokenManager tokenManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _offerPaymentUtils = offerPaymentUtils;
            _tokenManager = tokenManager;  
        }

        public GenericApiResponse InsertOfferPayment(string token, OfferPaymentDto offerPaymentDto)
        {
            try
            {
                return TryInsertOfferPayment(token,offerPaymentDto);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Couldn't Offer Payment", StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryInsertOfferPayment(string token, OfferPaymentDto offerPaymentDto)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            if (FindDuplicateOfferShare(offerPaymentDto) == null)
            {
                var offerPaymentToInsert =
                _offerPaymentUtils.ParseOfferPaymentDtoForInsert(
                    offerPaymentDto, extractedFromToken.UserLoginId
                );

                _repositoryWrapper
                   .OfferPaymentManager
                   .InsertOfferPayment(offerPaymentToInsert);
                return _responseManager.SuccessResponse(
                    "Successfull",
                    StatusCodes.Status200OK,
                    1
                );
            }

            throw new Exception("OfferShare is already Paid");
            
        }

        private OfferPayment? FindDuplicateOfferShare(OfferPaymentDto offerPaymentDto)
        {
            return _repositoryWrapper
                .OfferPaymentManager
                .GetOfferPaymentByOfferShareId(offerPaymentDto.OfferedShareId);
        }
    }
}