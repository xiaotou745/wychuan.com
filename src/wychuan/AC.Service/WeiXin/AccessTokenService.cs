using System;
using System.Net.Http;
using System.Threading.Tasks;
using AC.Cache;
using AC.Extension;
using AC.Helper;
using AC.Service.WeiXin.Common;
using Common.Logging;

namespace AC.Service.WeiXin
{
    /// <summary>
    /// 微信access_token获取服务
    /// </summary>
    public class AccessTokenService
    {
        #region 微信返回的access_token对象

        /// <summary>
        /// access_token 对象
        /// </summary>
        private class AccessToken
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
        }

        #endregion

        /// <summary>
        /// 缓存的key
        /// </summary>
        private const string ACCESS_TOKEN_CACHE_KEY = "ACCESS_TOKEN_CACHE_KEY";


        /// <summary>
        /// 获取微信的access_token值
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken()
        {
            try
            {
                object accessToken = DataCache.GetCache(ACCESS_TOKEN_CACHE_KEY);
                if (accessToken != null)
                {
                    return accessToken.ToString();
                }

                RefreshAccessToken().Wait();
                return DataCache.GetCache(ACCESS_TOKEN_CACHE_KEY).ToString();
            }
            catch (Exception ex)
            {
                AppLogger.LoggerOfWeiXin.Error("weixin access_token get error:", ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 刷新缓存中的access_token值
        /// </summary>
        /// <returns></returns>
        private static async Task RefreshAccessToken()
        {
            using (var httpClient = new HttpClient())
            {
                string accessTokenStr = await httpClient.GetStringAsync(WeiXinUrl.AccessTokenUrl);
                var accessToken = Json.JsonSerializer.Deserialize<AccessToken>(accessTokenStr);

                DataCache.SetCacheOfAbsolute(ACCESS_TOKEN_CACHE_KEY, accessToken.access_token,
                    DateTime.UtcNow.AddMinutes(110));

                //HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(weiXinAccessTokenUrl);
                //if (httpResponseMessage.IsSuccessStatusCode)
                //{
                //    string content = await httpResponseMessage.Content.ReadAsStringAsync();
                //    var accessToken = Json.JsonSerializer.Deserialize<AccessToken>(content);

                //    DataCache.SetCacheOfAbsolute(ACCESS_TOKEN_CACHE_KEY, accessToken.access_token,
                //        DateTime.UtcNow.AddMinutes(110));
                //}
            }
        }

    }
}