using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class OfferPaymentManager : IOfferPaymentManager
    {
        private readonly IGenericRepository<OfferPayment> _repositoryBase;

        public OfferPaymentManager(IGenericRepository<OfferPayment> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<OfferPayment> GetAllOfferPayments()
        {
            return _repositoryBase.GetAll().ToList();
        }

        public OfferPayment? GetOfferPayment(int offerPaymentId)
        {
            return _repositoryBase.GetById(offerPaymentId);
        }

        public OfferPayment? GetOfferPaymentByOfferShareId(int offerShareId)
        {
            return _repositoryBase.GetAll().FirstOrDefault(op => op.OfferedShareId == offerShareId);
        }

        public List<OfferPayment> GetOfferPaymentForUser(int userLoginId)
        {
            return _repositoryBase.GetAll().Where(op => op.UserLoginId == userLoginId).ToList();
        }

        public OfferPayment InsertOfferPayment(OfferPayment offerPayment)
        {
            var addedOfferPayment = _repositoryBase.Insert(offerPayment);
            _repositoryBase.Save();
            return addedOfferPayment;
        }
    }
}
