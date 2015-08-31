using System.Net.Http;
using AC.Extension;
using AC.Helper;
using AC.Service.WeiXin;
using AC.Service.WeiXin.Common;
using Common.Logging;
using NUnit.Framework;

namespace AC.Tests.Service.WeiXin
{
    [TestFixture]
    public class AccessTokenServiceTests
    {
        private readonly ILog logger = LogManager.GetCurrentClassLogger();

        [Test]
        public void GetTokenTest()
        {
            string accessToken = AccessTokenService.GetAccessToken();
            logger.InfoFormat("get accessToken:{0}", accessToken);

            //logger.InfoFormat("get accessToken2:{0}", AccessTokenService.GetAccessToken());
        }

        [Test]
        public async void HttpClientTest()
        {
            //using (HttpClient client = new HttpClient())
            //{
            //    string s = await client.GetStringAsync("http://www.wychuan.com");
            //    logger.Info(s);

            //    HttpResponseMessage httpResponseMessage = await client.GetAsync("http://www.wychuan.com");
            //    if (httpResponseMessage.IsSuccessStatusCode)
            //    {
            //        string s1 = await httpResponseMessage.Content.ReadAsStringAsync();
            //        logger.Info(s1);
            //    }
            //}

            using (HttpClient httpClient = new HttpClient())
            {
                //string s = await
                //    httpClient.GetStringAsync(
                //        "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wxda1b221c07aac68d&secret=0256c494ebd1c1b749c073f14e307f58");
                string s = await httpClient.GetStringAsync(WeiXinUrl.AccessTokenUrl);
                logger.Info(s);
            }
        }

    }
}
