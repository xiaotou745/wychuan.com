using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using AC.Helper;
using Common.Logging;

namespace AC.Service.WeiXin
{
    /// <summary>
    /// 微信身份验证服务
    /// </summary>
    public class WXAuthorizeService
    {
        private WXAuthorizeService()
        {
            
        }

        public static WXAuthorizeService Create()
        {
            return new WXAuthorizeService();
        }

        private readonly string wxtoken = ConfigHelper.GetConfigString("wxtoken", string.Empty);

        /// <summary>
        /// 验证请求者身份是否从微信服务器过来
        /// </summary>
        /// <returns></returns>
        public bool IsAuthorized()
        {
            NameValueCollection requestQueryParams = System.Web.HttpContext.Current.Request.QueryString;
            //如果没有timestamp or nonce or echostr三个参数中的任意一个，则验证失败；
            if (requestQueryParams.Count == 0
                || !requestQueryParams.AllKeys.Contains("timestamp")
                || !requestQueryParams.AllKeys.Contains("nonce")
                || !requestQueryParams.AllKeys.Contains("signature"))
            {
                return false;
            }
            
            /*加密/校验流程如下：
1. 将token、timestamp、nonce三个参数进行字典序排序
2. 将三个参数字符串拼接成一个字符串进行sha1加密
3. 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信*/
            var waitEncryptParamsArray = new[] { wxtoken, requestQueryParams["timestamp"], requestQueryParams["nonce"] };
            //排序
            string waitEncryptParamStr = string.Join(string.Empty, waitEncryptParamsArray.OrderBy(m => m));
            //sha1加密
            string encryptStr = AC.Security.HashAlgorithm.SHA1(waitEncryptParamStr);// HashEncode.GetSha1HashString(waitEncryptParamStr);
            AppLogger.LoggerOfWeiXin.InfoFormat("timestamp:{0} nonce:{1} signature:{2} encryptStr:{3}", requestQueryParams["timestamp"],
                requestQueryParams["nonce"], requestQueryParams["signature"], encryptStr);
            return encryptStr.ToLower().Equals(requestQueryParams["signature"].ToLower());
        }

    }
}
