using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DataContext
{
    public class CryptService
    {
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();

            foreach (var d in data)
            {
                sBuilder.Append(d.ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            var hashOfInput = GetMd5Hash(md5Hash, input);

            var comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(hashOfInput, hash);
        }
    }
}