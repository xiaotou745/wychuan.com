using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using AC.Helper;
using AC.Security;
using Common.Logging;

namespace AC.Web.Filters
{
    /// <summary>
    /// 微信公众平台接入验证
    /// </summary>
    public class WXAuthorizeAttribute : AuthorizeAttribute
    {
        private string wxtoken = ConfigHelper.GetConfigString("wxtoken", string.Empty);
        private ILog logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 验证请求者身份是否从微信服务器过来
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            Dictionary<string, string> requestQueryPairs = actionContext.Request.GetQueryNameValuePairs().ToDictionary(k => k.Key, v => v.Value);
            //如果没有timestamp or nonce or echostr三个参数中的任意一个，则验证失败；
            if (requestQueryPairs.Count == 0 
                || !requestQueryPairs.ContainsKey("timestamp")
                || !requestQueryPairs.ContainsKey("nonce")
                || !requestQueryPairs.ContainsKey("echostr"))
            {
                return false;
            }
            /*加密/校验流程如下：
1. 将token、timestamp、nonce三个参数进行字典序排序
2. 将三个参数字符串拼接成一个字符串进行sha1加密
3. 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信*/
            var waitEncryptParamsArray = new[] {wxtoken, requestQueryPairs["timestamp"], requestQueryPairs["nonce"]};
            //排序
            string waitEncryptParamStr = string.Join(string.Empty, waitEncryptParamsArray.OrderBy(m => m));
            logger.InfoFormat("wxtoken:{0} timestamp:{1} nonce:{2},waitEncryptParamStr:{3}", wxtoken, requestQueryPairs["timestamp"], requestQueryPairs["nonce"], waitEncryptParamStr);
            //sha1加密
            string encryptStr = AC.Security.HashAlgorithm.SHA1(waitEncryptParamStr);// HashEncode.GetSha1HashString(waitEncryptParamStr);
            logger.InfoFormat("encryptStr:{0} signature:{1}", encryptStr, requestQueryPairs["signature"]);
            return encryptStr.ToLower().Equals(requestQueryPairs["signature"].ToLower());
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.Unauthorized, new { status = "sign_error" });
        }
    }
}
