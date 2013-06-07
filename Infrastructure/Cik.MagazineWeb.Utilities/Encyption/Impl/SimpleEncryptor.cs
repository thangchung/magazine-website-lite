using System.Linq;
using System.Security.Cryptography;

namespace Cik.MagazineWeb.Utilities.Encyption.Impl
{
    public class SimpleEncryptor : IEncrypting
    {
        public string Encode(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                byte[] mang = System.Text.Encoding.UTF8.GetBytes(password);

                var myMd5 = new MD5CryptoServiceProvider();

                mang = myMd5.ComputeHash(mang);

                return mang.Aggregate("", (current, b) => current + b.ToString("X2"));
            }

            // TODO: logic code above should be double check again
            return string.Empty;
        } 
    }
}