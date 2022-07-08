
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IOfferTypeManager
    {
        List<OfferType> GetAllOfferTypes();
        OfferType GetOfferType(int id);

    }
}
