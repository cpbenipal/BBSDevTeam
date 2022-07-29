using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Utils
{
    public class GetBidOnPrimaryOfferUtils
    {
        private readonly IRepositoryWrapper _repository;

        public GetBidOnPrimaryOfferUtils(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public List<GetBidOnPrimaryOfferingDto> ParseBidsOnPrimaryShare(
            List<BidOnPrimaryOffering> bidOnPrimaryOfferings
        )
        {
            List<GetBidOnPrimaryOfferingDto> result = new();
            foreach (BidOnPrimaryOffering bidOnPrimary in bidOnPrimaryOfferings)
            {
                GetBidOnPrimaryOfferingDto bidOnPrimaryOfferingDto = BuildPrimaryBidOfferingsFromDto(bidOnPrimary);

                result.Add(bidOnPrimaryOfferingDto);
            }

            return result;
        }

        public GetBidOnPrimaryOfferingDto BuildPrimaryBidOfferingsFromDto(
            BidOnPrimaryOffering bidOnPrimary
        )
        {
            var paymentType = _repository.PaymentTypeManager.GetPaymentType(bidOnPrimary.PaymentTypeId);
            var verificationState = _repository.StateManager.GetState(bidOnPrimary.VerificationStatus);
            var company = _repository.CompanyManager.GetCompany(bidOnPrimary.CompanyId);
            var approvedDate = bidOnPrimary.ApprovedOn.ToShortDateString();

            return new GetBidOnPrimaryOfferingDto
            {
                Id = bidOnPrimary.Id,
                ApprovedOn = approvedDate,
                CompanyName = company?.Name ?? "",
                PaymentType = paymentType?.Value ?? "",
                PlacementAmount = bidOnPrimary.PlacementAmount,
                TransactionId = bidOnPrimary.TransactionId,
                UserLoginId = bidOnPrimary.UserLoginId,  
                IsDownload = bidOnPrimary.IsDownload,
                IsESign = bidOnPrimary.IsESign,
                VerificationStatus = verificationState?.Name ?? ""
            };
        }
    }
}
