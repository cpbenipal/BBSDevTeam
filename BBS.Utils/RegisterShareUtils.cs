using BBS.Dto;
using BBS.Models;

namespace BBS.Utils
{
    public static class RegisterShareUtils
    {
        public static Share ParseShareObjectFromRegisterShareDto(
            RegisterShareDto registerShareDto
        )
        {
            Share share = new()
            {
                CompanyName = registerShareDto.ShareInformation.CompanyName,
                SharePrice = registerShareDto.ShareInformation.SharePrice,
                StorageLocationId = registerShareDto.ShareInformation.StorageLocationId,
                DateOfGrant = registerShareDto.ShareInformation.DateOfGrant,
                NumberOfShares = registerShareDto.ShareInformation.NumberOfShares,
                GrantTypeId = registerShareDto.ShareInformation.GrantTypeId,
                DebtRoundId = registerShareDto.ShareInformation.DebtRoundId,
                EquityRoundId = registerShareDto.ShareInformation.EquityRoundId,
                Restriction1 = registerShareDto.ShareInformation.Restriction1,
                Restriction2 = registerShareDto.ShareInformation.Restriction2,
                FirstName = registerShareDto.ContactPerson?.FirstName,
                LastName = registerShareDto.ContactPerson?.LastName,
                Email = registerShareDto.ContactPerson?.Email,
                PhoneNumber = registerShareDto.ContactPerson?.PhoneNumber,
                GrantValuation = registerShareDto.ShareInformation?.GrantValuation ?? "",
                LastValuation = registerShareDto.ShareInformation?.LastValuation ?? "",
            };

            return share;
        }

    }
}
