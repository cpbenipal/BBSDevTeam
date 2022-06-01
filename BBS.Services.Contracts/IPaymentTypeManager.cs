
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IPaymentTypeManager
    {
        PaymentType? GetPaymentType(int paymentTypeId);
        List<PaymentType> GetAllPaymentTypes();
    }
}
