﻿using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllCompaniesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetAllCompaniesInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
        }

        public GenericApiResponse GetAllCompanies()
        {
            try
            {
                return TryGettingAllCompanies();
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
                "Error In Fetching Companies", StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllCompanies()
        {
            var allCompanies = _repositoryWrapper.CompanyManager.GetCompanies();
            return _responseManager.SuccessResponse("Successfull", StatusCodes.Status200OK, allCompanies);
        }
    }
}