using BBS.Services.Contracts;
using System.Text;

namespace BBS.Services.Repository
{
    public class HashManager : IHashManager
    {
        public string DecryptCipherText(string cipherText)
        {
            UTF8Encoding utfEncoding = new UTF8Encoding();
            Decoder Decode = utfEncoding.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(cipherText);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
          //  string decryptpwd = new(decoded_char);
            return string.Join("", decoded_char);
        }

        public string EncryptPlainText(string plainText)
        {
            byte[] encode = Encoding.UTF8.GetBytes(plainText);
            string cipherText = Convert.ToBase64String(encode);
            return cipherText;
        }

        public List<byte[]> HashWithSalt(string password)
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

        public bool VerifyPasswordWithSaltAndStoredHash(string password, byte[] storedHash, byte[] storedSalt)
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
