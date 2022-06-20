using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IIssuedDigitalShareManager
    {
        IssuedDigitalShare InsertDigitallyIssuedShare(IssuedDigitalShare issuedShare);
        List<IssuedDigitalShare> GetIssuedDigitalSharesByShareId(int shareId); 
        List<IssuedDigitalShare> GetIssuedDigitalSharesForPerson(int userLoginId);
        List<IssuedDigitalShare> GetAllIssuedDigitalShares();
        string GetIssuedDigitalShareCertificateUrl(int issuedDigitalShareId);
        IssuedDigitalShare GetIssuedDigitalShare(int id);
    }
}
