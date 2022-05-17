using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IIssuedDigitalShareManager
    {
        IssuedDigitalShare InsertDigitallyIssuedShare(IssuedDigitalShare issuedShare);
        List<IssuedDigitalShare> GetIssuedDigitalSharesByShareIdAndCompanyId(int shareId, int companyId);
        List<IssuedDigitalShare> GetIssuedDigitalSharesForPerson(int userLoginId);
        string GetIssuedDigitalShareCertificateUrl(int issuedDigitalShareId);
    }
}
