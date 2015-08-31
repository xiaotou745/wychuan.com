using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AC.Service.WeiXin;
using AC.Service.WeiXin.Response;
using AC.Web.Common.Filter;

namespace wychuan2.com.Areas.weixin.Controllers.api
{
    [WeiXinMsgLog]
    public class ProcessorController : ApiController
    {
        [HttpPost]
        [HttpGet]
        public HttpResponseMessage Index()
        {
            if (Request.Method == HttpMethod.Get)
            {
                #region 如果是get请求,则验证token,只有要配置微信服务器的时候,才会使用到;
                var requestQueryPairs = Request.GetQueryNameValuePairs().ToDictionary(k => k.Key, v => v.Value);
                #region 可以不验证token,直接返回成功
                //return new HttpResponseMessage(HttpStatusCode.OK)
                //    {
                //        Content = new StringContent(requestQueryPairs["echostr"]),
                //    };
                #endregion
                if (WXAuthorizeService.Create().IsAuthorized())
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(requestQueryPairs["echostr"]),
                    };
                }
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("sign_error"),
                };
                #endregion
            }

            Task<string> readAsStringAsync = Request.Content.ReadAsStringAsync();
            readAsStringAsync.Wait();
            string response = MsgResponseBuilder.Builder(readAsStringAsync.Result)
                .GetResponse();
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(response)
            };
        }

        /// <summary>
        /// 获取accesstoken
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetAccessToken()
        {
            return AccessTokenService.GetAccessToken();
        }

        [HttpGet]
        public string test()
        {
            return "hello world";
        }
    }
}
