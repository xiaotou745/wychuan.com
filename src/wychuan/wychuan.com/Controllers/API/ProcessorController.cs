using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Security;
using AC.Web.Filters;

namespace AC.Web.Controllers.API
{
    public class ProcessorController : ApiController
    {
        // GET api/processor
        public IEnumerable<string> Get()
        {
            string tempStr = "1436454968189615242wangyuchun198312";
            List<string> result = new List<string>();

            result.Add(tempStr);

            HashAlgorithm algorithm = SHA1.Create();  // SHA1.Create()
            byte[] computeHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(tempStr));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in computeHash)
                sb.Append(b.ToString("X2"));
            result.Add(sb.ToString());

            result.Add(Sha1Hash(tempStr));

            result.Add(Hash(tempStr));

            result.Add(CalculateSHA1(tempStr, Encoding.UTF8));

            result.Add(SHA1Sign(tempStr));

            result.Add(AC.Security.HashAlgorithm.SHA1(tempStr));

            result.Add(FormsAuthentication.HashPasswordForStoringInConfigFile(tempStr, "SHA1"));
            return result;
        }

        public static string SHA1Sign(string data)
        {
            byte[] temp1 = Encoding.UTF8.GetBytes(data);

            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            byte[] temp2 = sha.ComputeHash(temp1);
            sha.Clear();

            // 注意， 不能用这个
            //string output = Convert.ToBase64String(temp2);

            string output = BitConverter.ToString(temp2);
            output = output.Replace("-", "");
            output = output.ToLower();
            return output;
        }

        public static string CalculateSHA1(string text, Encoding enc)
        {
            byte[] buffer = enc.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
        }

        string Hash(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

        //hex encoding of the hash, in uppercase.
public static string Sha1Hash (string str)
{
    byte[] data = UTF8Encoding.UTF8.GetBytes (str);
    data = Sha1Hash (data);
    return BitConverter.ToString (data).Replace ("-", "");
}
// Do the actual hashing
        public static byte[] Sha1Hash(byte[] data)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                return sha1.ComputeHash(data);
            }
        }
    }
}
