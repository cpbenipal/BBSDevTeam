namespace BBS.Services.Contracts
{
    public interface IHashManager
    {
        List<byte[]> Hash(string password);
        bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt);
    }
}
