using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AC.Service.WeiXin;
using AC.Web.Filters;
using Common.Logging;

namespace AC.Web.Controllers.API
{
    public class WeiXinController : ApiController
    {
        [HttpGet]
        [HttpPost]
        // GET api/weixin/processor
        public HttpResponseMessage Processor()
        {
            ILog logger = LogManager.GetLogger("WeixinLog");
            if (Request.Method == HttpMethod.Get)
            {
                var requestQueryPairs = Request.GetQueryNameValuePairs().ToDictionary(k => k.Key, v => v.Value);
                //return new HttpResponseMessage(HttpStatusCode.OK)
                //    {
                //        Content = new StringContent(requestQueryPairs["echostr"]),
                //    };
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
            }
            Task<string> readAsStringAsync = Request.Content.ReadAsStringAsync();
            readAsStringAsync.Wait();
            logger.InfoFormat("post:{0}", readAsStringAsync.Result);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
