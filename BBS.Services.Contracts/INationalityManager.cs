using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface INationalityManager
    {
        Nationality InsertNationality(Nationality nationality);
    }
}
