using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Utils
{
    public class GetRegisteredSharesUtils
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IFileUploadService _uploadService;
        public GetRegisteredSharesUtils(
            IRepositoryWrapper repositoryWrapper, 
            IFileUploadService fileUploadService
        )
        {
            _repository = repositoryWrapper;
            _uploadService = fileUploadService;
        }

        public List<RegisteredShareDto> MapListOfSharesToListOfRegisteredSharesDto(List<Share> shares)
        {
            var allMappedShares = new List<RegisteredShareDto>();
            shares.ForEach(e =>
            {
                RegisteredShareDto registeredShare = BuildShareDtoObject(e);
                allMappedShares.Add(registeredShare);
            });

            return allMappedShares;
        }

        public RegisteredShareDto BuildShareDtoObject(Share share)
        {
            var debtRound =  share.DebtRoundId == null ?  null :
                _repository.DebtRoundManager.GetDebtRound((int)share.DebtRoundId!);
            var equityRound = share.EquityRoundId == null ? null : 
                _repository.EquityRoundManager.GetEquityRound((int)share.EquityRoundId!);
            var grantType = _repository.GrantTypeManager.GetGrantType(share.GrantTypeId);
            var restriction = _repository.RestrictionManager.GetAllRestrictions();
            var storageLocation = _repository.StorageLocationManager.GetStorageLocation(share.GrantTypeId);
            var state = _repository.StateManager.GetState(share.VerificationState);

            var restrictions = new List<RestrictionDto>
            {
                new RestrictionDto() { Id = restriction[0].Id, Name = restriction[0].Name, Flag = share.Restriction1 },
                new RestrictionDto() { Id = restriction[1].Id, Name = restriction[1].Name, Flag = share.Restriction2 }
            };

            var registeredShare = new RegisteredShareDto
            {
                Id = share.Id,
                BusinessLogo = !string.IsNullOrEmpty(share.BusinessLogo) ?
                    _uploadService.GetFilePublicUri(share.BusinessLogo!) : null,
                FirstName = share?.FirstName,
                LastName = share?.LastName,
                Email = share?.Email,
                DebtRound = debtRound?.Name,
                EquityRound = equityRound?.Name,
                ShareOwnerShipDocument = !string.IsNullOrEmpty(share!.ShareOwnershipDocument) ?
                    _uploadService.GetFilePublicUri(share!.ShareOwnershipDocument!) : null,
                CompanyInformationDocument = !string.IsNullOrEmpty(share!.CompanyInformationDocument) ? 
                    _uploadService.GetFilePublicUri(share.CompanyInformationDocument!) : null,
                CompanyName = share.CompanyName,
                DateOfGrant = share.DateOfGrant.Day + " of " + share.DateOfGrant.ToString("MMMM") + " " + share.DateOfGrant.Year,
                GrantType = grantType.Name,
                NumberOfShares = share.NumberOfShares,
                PhoneNumber = share?.PhoneNumber,
                Restriction = restrictions,
                SharePrice = share!.SharePrice,
                StorageLocation = storageLocation.Name,
                VerficationStatus = state.Name,
                GrantValuation = share.GrantValuation ?? "",
                LastValuation = share.LastValuation ?? "",
                UserLoginId = share.UserLoginId,
                AddedDate = share.AddedDate.ToShortDateString()
            };
            return registeredShare;
        }
    }
}
