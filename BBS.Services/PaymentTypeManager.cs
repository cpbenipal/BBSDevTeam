using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class PaymentTypeManager : IPaymentTypeManager
    {
        private readonly IGenericRepository<PaymentType> _repositoryBase;

        public PaymentTypeManager(IGenericRepository<PaymentType> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<PaymentType> GetAllPaymentTypes()
        {
            return _repositoryBase.GetAll().ToList();
        }

        public PaymentType? GetPaymentType(int paymentTypeId)
        {
            return _repositoryBase.GetById(paymentTypeId);
        }
    }
}
