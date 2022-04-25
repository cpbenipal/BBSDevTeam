using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Utils
{
    public class SecureUtil
    {
        public static string Encrypt(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

        public static string Decrypt(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        public class SendGridEmailSenderOptions
        {
            public string ApiKey { get; set; }

            public string SenderEmail { get; set; }

            public string SenderName { get; set; }
        }
    }
}
 