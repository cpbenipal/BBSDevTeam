using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IIssuedDigitalShareManager
    {
        IssuedDigitalShare InsertDigitallyIssuedShare(IssuedDigitalShare issuedShare);
        List<IssuedDigitalShare> GetIssuedDigitalSharesByShareIdAndCompanyId(int shareId, string companyName); 
        List<IssuedDigitalShare> GetIssuedDigitalSharesForPerson(int userLoginId);
        List<IssuedDigitalShare> GetAllIssuedDigitalShares();
        string GetIssuedDigitalShareCertificateUrl(int issuedDigitalShareId);
        IssuedDigitalShare GetIssuedDigitalShare(int id);
    }
}
