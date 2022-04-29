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
            string certificateUrl
        )
        {
            var mappedShare = _mapper.Map<IssuedDigitalShare>(issueDigitalShareDto);
            mappedShare.UserLoginId = userLoginId;
            mappedShare.CertificateUrl = certificateUrl;

            return mappedShare;
        }
    }
}
