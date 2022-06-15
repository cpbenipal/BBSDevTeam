using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllInvestorsDetailsInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;

        public GetAllInvestorsDetailsInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
        }

        public GenericApiResponse GetAllInvestorsDetails(string token)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
            try
            {
                _loggerManager.LogInfo(
                    "GetAllInvestorsDetails : " +
                    CommonUtils.JSONSerialize("No Body"),
                    extractedFromToken.PersonId
                );
                return TryAllGettingInvestorDetails(extractedFromToken);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Couldn't Get Investors Detail", 
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryAllGettingInvestorDetails(TokenValues extractedFromToken)
        {

            if(extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                throw new Exception("Access Denied.");
            }

            var allPerson = _repositoryWrapper.PersonManager.GetAllPerson();
            var allInvestors = 
                allPerson.Where(p => 
                GetUserRoleByPerson(p.Id)!.RoleId == (int)Roles.INVESTOR
            ).ToList();

            var pendingAccounts = allInvestors.Where(
                i => i.VerificationState == (int)States.PENDING
            ).ToList();

            var approvedAccounts = allInvestors.Where(
                i => i.VerificationState == (int)States.COMPLETED
            ).ToList();

            var highRiskAccounts = _repositoryWrapper.InvestorDetailManager
                .GetInvestorDetails().Where(
                i => i.InvestorRiskType == (int)InvestorRiskTypes.HIGH_RISK
            ).ToList();

            var normalAccounts = _repositoryWrapper.InvestorDetailManager
                .GetInvestorDetails().Where(
                i => i.InvestorRiskType == (int)InvestorRiskTypes.NORMAL
            ).ToList();

            var retailInvestors = _repositoryWrapper.InvestorDetailManager
                .GetInvestorDetails().Where(
                i => i.InvestorType == (int)InvestorTypes.RETAIL
            ).ToList();

            var qualifiedInvestors = _repositoryWrapper.InvestorDetailManager
                .GetInvestorDetails().Where(
                i => i.InvestorType == (int)InvestorTypes.QUALIFIED
            ).ToList();


            var getInvestorDetail = new GetInvestorDetailDto
            {
                ApprovedAccountsCount = approvedAccounts.Count,
                HighRiskInvestorsCount = highRiskAccounts.Count,
                InvestorsCount = allInvestors.Count,
                NormalInvestorsCount = normalAccounts.Count,
                PendingAccountsCount = pendingAccounts.Count,
                QualifiedInvestorsCount = qualifiedInvestors.Count,
                RetailInvestorsCount = retailInvestors.Count
            };

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                getInvestorDetail
            );
        }

        private UserRole? GetUserRoleByPerson(int personId)
        {
            var userLogin = _repositoryWrapper.UserLoginManager.GetUserLoginByPerson(personId);
            var userRole = _repositoryWrapper.UserRoleManager.GetUserRoleByUserLoginId(userLogin!.Id);

            return userRole;
        }
    }
}
