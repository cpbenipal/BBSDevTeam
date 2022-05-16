using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Utils
{
    public class GetIssuedDigitalSharesUtils
    {
        private readonly IFileUploadService _uploadService;

        public GetIssuedDigitalSharesUtils(IFileUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        public List<GetDigitalSharesItemDto> ParseDigitalSharesToDto(List<IssuedDigitalShare> digitalShares)
        {
            List<GetDigitalSharesItemDto> result = new();
            foreach (IssuedDigitalShare digitalShare in digitalShares)
            {
                var getDigitalSharesItemDto = new GetDigitalSharesItemDto
                {
                    Id = digitalShare.Id,
                    CertificateKey = digitalShare.CertificateKey,
                    CertificateUrl = _uploadService.GetFilePublicUri(digitalShare.CertificateUrl),
                    CompanyName = digitalShare.CompanyName,
                    DateOfBirth = digitalShare.DateOfBirth.ToString("YYYY-MM-DD"),
                    FirstName = digitalShare.FirstName,
                    MiddleName = digitalShare.MiddleName,
                    LastName = digitalShare.LastName,
                    IsCertified = digitalShare.IsCertified,
                    ShareId = digitalShare.ShareId,
                };

                result.Add(getDigitalSharesItemDto);
            }

            return result;
        }
    }
}
