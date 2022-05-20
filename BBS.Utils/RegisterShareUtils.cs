using BBS.Dto;
using BBS.Models;

namespace BBS.Utils
{
    public class RegisterShareUtils
    {
        public static Share ParseShareObjectFromRegisterShareDto(
            RegisterShareDto registerShareDto
        )
        {
            Share share = new()
            {
               // CompanyId = registerShareDto.ShareInformation.CompanyId,
                CompanyName = registerShareDto.ShareInformation.CompanyName,
                SharePrice = registerShareDto.ShareInformation.SharePrice,
                StorageLocationId = registerShareDto.ShareInformation.StorageLocationId,
                DateOfGrant = registerShareDto.ShareInformation.DateOfGrant,
                NumberOfShares = registerShareDto.ShareInformation.NumberOfShares,
                GrantTypeId = registerShareDto.ShareInformation.GrantTypeId,
                DebtRoundId = registerShareDto.ShareInformation.DebtRoundId,
                EquityRoundId = registerShareDto.ShareInformation.EquityRoundId,
                RestrictionId = registerShareDto.ShareInformation.RestrictionId,

                FirstName = registerShareDto.ContactPerson.FirstName,
                LastName = registerShareDto.ContactPerson.LastName,
                Email = registerShareDto.ContactPerson.Email,
                PhoneNumber = registerShareDto.ContactPerson.PhoneNumber
            };

            return share;
        }

    }
}
