namespace BBS.Services.Contracts
{
    public interface ISMSSender
    {
        Task Send(string phoneNumber, string message);
    }
}
