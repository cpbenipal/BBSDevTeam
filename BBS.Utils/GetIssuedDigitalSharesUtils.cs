using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Utils
{
    public class GetIssuedDigitalSharesUtils
    {
        private readonly IFileUploadService _uploadService;
        private readonly IRepositoryWrapper _repository;

        public GetIssuedDigitalSharesUtils(
            IFileUploadService uploadService, 
            IRepositoryWrapper repository
        )
        {
            _uploadService = uploadService;
            _repository = repository;
        }

        public List<GetDigitalSharesItemDto> ParseDigitalSharesToDto(List<IssuedDigitalShare> digitalShares)
        {
            List<GetDigitalSharesItemDto> result = new();
            foreach (IssuedDigitalShare digitalShare in digitalShares)
            {
                GetDigitalSharesItemDto getDigitalSharesItemDto = BuildDigitalShareFromDto(digitalShare);

                result.Add(getDigitalSharesItemDto);
            }

            return result;
        }

        public GetDigitalSharesItemDto BuildDigitalShareFromDto(IssuedDigitalShare digitalShare)
        {

            var share = _repository.ShareManager.GetShare(digitalShare.ShareId);

            return new GetDigitalSharesItemDto
            {
                Id = digitalShare.Id,
                CertificateKey = digitalShare.CertificateKey,
                CertificateUrl = _uploadService.GetFilePublicUri(digitalShare.CertificateUrl),
                FirstName = share.FirstName,
                LastName = share.LastName,
                IsCertified = digitalShare.IsCertified,
                ShareId = share.Id,
                NumberOfShares = share.NumberOfShares,
                CompanyName = share.CompanyName,
            };
        }
    }
}
