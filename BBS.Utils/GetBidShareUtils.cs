using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Utils
{
    public class GetBidShareUtils
    {
        private readonly IFileUploadService _uploadService;
        private readonly IRepositoryWrapper _repository;

        public GetBidShareUtils(IFileUploadService uploadService, IRepositoryWrapper repository)
        {
            _uploadService = uploadService;
            _repository = repository;
        }

        public List<GetBidShareDto> ParseBidSharesToDto(List<BidShare> bidShares)
        {
            List<GetBidShareDto> result = new();
            foreach (BidShare bidShare in bidShares)
            {
                GetBidShareDto bidShareDto = BuildBidShareFromDto(bidShare);

                result.Add(bidShareDto);
            }

            return result;
        }

        public GetBidShareDto BuildBidShareFromDto(BidShare bidShare)
        {
            var paymentType = _repository.PaymentTypeManager.GetPaymentType(bidShare.PaymentTypeId);
            var verificationState = _repository.StateManager.GetState(bidShare.VerificationStateId);

            var offeredShare = _repository.OfferedShareManager.GetOfferedShare(bidShare.OfferedShareId);

            var digitallyIssuedShare = _repository
                .IssuedDigitalShareManager
                .GetIssuedDigitalShare(offeredShare.IssuedDigitalShareId);

            var share = _repository.ShareManager.GetShare(digitallyIssuedShare.ShareId);

            return new GetBidShareDto
            {
                MaximumBidPrice = bidShare.MaximumBidPrice,
                MinimumBidPrice = bidShare.MinimumBidPrice,
                Id = bidShare.Id,
                OfferedShareId = bidShare.OfferedShareId,
                PaymentType = paymentType!.Value,
                Quantity = bidShare.Quantity,
                VerificationState = verificationState.Name,
                CompanyName = share.CompanyName,
                BusinessLogo = _uploadService.GetFilePublicUri(share.BusinessLogo!),
            };
        }
    }
}
