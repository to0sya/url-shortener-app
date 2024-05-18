using System.Security.Cryptography;
using System.Text;

namespace url_shortener.Utilities
{
    public static class ShortUrlService
    {
        public static string Short(string url)
        {
            string result;
            using (MD5 md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(url));
                result = Convert.ToBase64String(hash)
                    .Replace('+', '-')
                    .Replace('/', '_')
                    .Substring(0, 8);
            }
            return result;
        }
    }
}
