using AutoMapper;
using BBS.Dto;
using BBS.Models;

namespace BBS.Utils
{
    public class IssueDigitalShareUtils
    {
        private readonly IMapper _mapper;
        public IssueDigitalShareUtils(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IssuedDigitalShare MapDigitalShareObjectFromRequest(
            IssueDigitalShareDto issueDigitalShareDto, 
            int userLoginId,
            string certificateUrl,
            string certificateKey
        )
        {
            var mappedShare = _mapper.Map<IssuedDigitalShare>(issueDigitalShareDto);
            mappedShare.UserLoginId = userLoginId;
            mappedShare.CertificateUrl = certificateUrl;
            mappedShare.CertificateKey = certificateKey;
            mappedShare.AddedById = userLoginId;
            mappedShare.ModifiedById = userLoginId;               
            return mappedShare;
        }

        public static string GetFilenameFromUrl(string url)
        {
            return String.IsNullOrEmpty(url.Trim()) || !url.Contains('.') ? string.Empty : Path.GetFileName(new Uri(url).AbsolutePath);
        }
    }
}
