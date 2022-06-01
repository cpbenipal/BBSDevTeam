
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IOfferPaymentManager
    {
        OfferPayment InsertOfferPayment(OfferPayment offerPayment);
        OfferPayment? GetOfferPayment(int offerPaymentId);
        OfferPayment? GetOfferPaymentByOfferShareId(int offerShareId);
        List<OfferPayment> GetOfferPaymentForUser(int userLoginId);
        List<OfferPayment> GetAllOfferPayments();
    }
}
