
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IOfferTimeLimitManager
    {
        OfferTimeLimit? GetOfferTimeLimit(int id);
        List<OfferTimeLimit> GetAllOfferTimeLimits();
    }
}
