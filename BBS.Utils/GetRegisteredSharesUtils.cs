using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Utils
{
    public class GetRegisteredSharesUtils
    {
        private IRepositoryWrapper _repository;
        public GetRegisteredSharesUtils(IRepositoryWrapper repositoryWrapper)
        {
            _repository = repositoryWrapper;
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

        private RegisteredShareDto BuildShareDtoObject(Share share)
        {
            var debtRound = _repository.DebtRoundManager.GetDebtRound(share.DebtRoundId);
            var equityRound = _repository.EquityRoundManager.GetEquityRound(share.EquityRoundId);
            var grantType = _repository.GrantTypeManager.GetGrantType(share.GrantTypeId);
            var restriction = _repository.RestrictionManager.GetRestriction(share.RestrictionId);
            var storageLocation = _repository.StorageLocationManager.GetStorageLocation(share.GrantTypeId);

            var registeredShare = new RegisteredShareDto
            {
                BusinessLogo = share.BusinessLogo,
                FirstName = share.FirstName,
                LastName = share.LastName,
                Email = share.Email,
                DebtRound = debtRound.Name,
                EquityRound = equityRound.Name,
                ShareOwnerShipDocument = share.ShareOwnershipDocument,
                CompanyInformationDocument = share.CompanyInformationDocument,
                CompanyName = share.CompanyName,
                DateOfGrant = share.DateOfGrant.Day + " of " + share.DateOfGrant.ToString("MMMM") + " " + share.DateOfGrant.Year,
                GrantType = grantType.Name,
                NumberOfShares = share.NumberOfShares,
                PhoneNumber = share.PhoneNumber,
                Restriction = restriction?.Name ?? "",
                SharePrice = share.SharePrice,
                StorageLocation = storageLocation.Name,
            };
            return registeredShare;
        }
    }
}
