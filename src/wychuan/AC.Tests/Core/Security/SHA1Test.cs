using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using Common.Logging;
using NUnit.Framework;

namespace AC.Tests.Core.Security
{
    [TestFixture]
    public class SHA1Test
    {
        private readonly ILog logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     SHA1算法1
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        private string Sha11(string sourceStr)
        {
            HashAlgorithm algorithm = SHA1.Create();
            byte[] computeHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(sourceStr));
            var sb = new StringBuilder();
            foreach (byte b in computeHash)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        //hex encoding of the hash, in uppercase.
        private string Sha12(string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            using (var sha1 = new SHA1Managed())
            {
                return BitConverter.ToString(sha1.ComputeHash(data)).Replace("-", string.Empty);
            }
        }

        private string Sha13(string input)
        {
            byte[] hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

        private string Sha14(string text, Encoding enc)
        {
            byte[] buffer = enc.GetBytes(text);
            var cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
        }

        [Test]
        public void Sha1Test()
        {
            string tempStr = "1436454968189615242wangyuchun198312";

            logger.InfoFormat("算法1：{0}", Sha11(tempStr));
            logger.InfoFormat("算法2：{0}", Sha12(tempStr));
            logger.InfoFormat("算法3：{0}", Sha13(tempStr));
            logger.InfoFormat("算法4：{0}", Sha14(tempStr, Encoding.UTF8));
            logger.InfoFormat("AC.Security.HashAlgorithm.SHA1：{0}", AC.Security.HashAlgorithm.SHA1(tempStr));
            logger.InfoFormat("FormsAuthentication.HashPasswordForStoringInConfigFile：{0}",
                FormsAuthentication.HashPasswordForStoringInConfigFile(tempStr, "SHA1"));
        }
    }
}