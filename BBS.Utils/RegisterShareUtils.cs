using BBS.Dto;
using BBS.Models;

namespace BBS.Utils
{
    public class RegisterShareUtils
    {
        public Share ParseShareObjectFromRegisterShareDto(RegisterShareDto registerShareDto)
        {
            Share share = new Share();

            share.CompanyId = registerShareDto.ShareInformation.CompanyId;
            share.CompanyName = registerShareDto.ShareInformation.CompanyName;
            share.SharePrice = registerShareDto.ShareInformation.SharePrice;
            share.StorageLocationId = registerShareDto.ShareInformation.StorageLocationId;
            share.DateOfGrant = registerShareDto.ShareInformation.DateOfGrant;
            share.NumberOfShares = registerShareDto.ShareInformation.NumberOfShares;
            share.GrantTypeId = registerShareDto.ShareInformation.GrantTypeId;
            share.DebtRoundId = registerShareDto.ShareInformation.DebtRoundId;
            share.EquityRoundId = registerShareDto.ShareInformation.EquityRoundId;
            share.RestrictionId = registerShareDto.ShareInformation.RestrictionId;

            share.FirstName = registerShareDto.ContactPerson.FirstName;
            share.LastName = registerShareDto.ContactPerson.LastName;
            share.Email = registerShareDto.ContactPerson.Email;
            share.PhoneNumber = registerShareDto.ContactPerson.PhoneNumber;

            share.AddedDate = DateTime.Now;


            return share;
        }

    }
}
