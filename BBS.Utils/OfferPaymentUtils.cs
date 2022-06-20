using AutoMapper;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Utils
{
    public class OfferPaymentUtils
    {
        public readonly IMapper _mapper;
        public readonly IRepositoryWrapper _repository;

        public OfferPaymentUtils(IMapper mapper, IRepositoryWrapper repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public OfferPayment ParseOfferPaymentDtoForInsert(OfferPaymentDto offerPaymentDto, int userLoginId)
        {
            var mapped = _mapper.Map<OfferPayment>(offerPaymentDto);
            mapped.TransactionId = Guid.NewGuid().ToString("N").Replace("-", "").ToUpper();
            mapped.UserLoginId = userLoginId;

            return mapped;
        }


        public List<GetOfferPaymentDto> ParseGetOfferPaymentDtoList(
            List<OfferPayment> offerPayments
        )
        {
            List<GetOfferPaymentDto> offerPaymentDtoList = new();
            foreach (var offerPaymentDto in offerPayments)
            {
                var parsed = BuildGetOfferPaymentDto(offerPaymentDto);
                offerPaymentDtoList.Add(parsed);
            }

            return offerPaymentDtoList;
        }

        public GetOfferPaymentDto BuildGetOfferPaymentDto(OfferPayment offerPayment)
        {
            var paymentType = _repository.PaymentTypeManager.GetPaymentType(offerPayment.PaymentTypeId);
            var offeredShare = _repository.OfferedShareManager.GetOfferedShare(offerPayment.OfferedShareId);
            var issuedShare = _repository.IssuedDigitalShareManager.GetIssuedDigitalShare(offeredShare.IssuedDigitalShareId);
            var share = _repository.ShareManager.GetShare(issuedShare.ShareId);

            return new GetOfferPaymentDto
            {
                Id = offerPayment.Id,
                OfferedShareId = offerPayment.OfferedShareId,
                PaymentType = paymentType!.Value,
                TransactionId = offerPayment.TransactionId,
                CompanyName = share.CompanyName
            };
        }
    }
}
