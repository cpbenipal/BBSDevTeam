namespace BBS.Services.Contracts
{
    public interface ISmsSender
    {
        Task Send(string phoneNumber, string message);
    }
}
