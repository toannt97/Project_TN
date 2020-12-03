using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace OnlineShoppingWebAPI.Common
{
    public class Encrypt
    {
        public static string CreateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            var buffer = new byte[size];
            rng.GetBytes(buffer);
            return Convert.ToBase64String(buffer);

        }

        public static string SHA256Hash(string textInput)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(textInput);
            SHA256Managed sha256HashString = new SHA256Managed();
            byte[] hashValue = sha256HashString.ComputeHash(bytes);
            return BitConverter.ToString(hashValue).Replace("-", "");
        }
    }
}
