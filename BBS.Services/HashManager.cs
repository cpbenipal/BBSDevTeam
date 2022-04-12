using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class HashManager : IHashManager
    {
        public List<byte[]> Hash(string password)
        {
            var hash = new List<byte[]>();

            if (password == null) 
                throw new ArgumentNullException("Password Is Empty");
            if (string.IsNullOrWhiteSpace(password)) 
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var hashOne = hmac.Key;
                var hashTwo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                hash.Add(hashOne);
                hash.Add(hashTwo);
            }

            return hash;
        }

        public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) 
                throw new ArgumentNullException("Password Is Empty");
            if (string.IsNullOrWhiteSpace(password)) 
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) 
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) 
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
