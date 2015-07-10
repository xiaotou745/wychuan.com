using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AC.Web.Filters;

namespace AC.Web.Controllers.API
{
    public class WeiXinController : ApiController
    {
        [WXAuthorize]
        [HttpGet]
        // GET api/weixin/processor
        public HttpResponseMessage Processor()
        {
            var requestQueryPairs = Request.GetQueryNameValuePairs().ToDictionary(k => k.Key, v => v.Value);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(requestQueryPairs["echostr"]),
            };
        }
    }
}
