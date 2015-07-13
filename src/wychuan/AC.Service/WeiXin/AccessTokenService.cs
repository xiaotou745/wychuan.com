﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using AC.Cache;
using AC.Extension;
using AC.Helper;
using Common.Logging;

namespace AC.Service.WeiXin
{
    /// <summary>
    /// 微信access_token获取服务
    /// </summary>
    public class AccessTokenService
    {
        /// <summary>
        /// access_token 对象
        /// </summary>
        private class AccessToken
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
        }
        /// <summary>
        /// 缓存的key
        /// </summary>
        private const string ACCESS_TOKEN_CACHE_KEY = "ACCESS_TOKEN_CACHE_KEY";
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();

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
                Logger.Error("weixin access_token get error:", ex);
                return string.Empty;
            }
        }
        /// <summary>
        /// 刷新缓存中的access_token值
        /// </summary>
        /// <returns></returns>
        private static async Task RefreshAccessToken()
        {
            string weiXinAccessTokenUrl = GetWeiXinAccessTokenUrl();
            using (var httpClient = new HttpClient())
            {
                string accessTokenStr = await httpClient.GetStringAsync(weiXinAccessTokenUrl);
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
        /// <summary>
        /// 获取微信access_token值的url
        /// </summary>
        /// <returns></returns>
        private static string GetWeiXinAccessTokenUrl()
        {
            const string tempStr = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
            return tempStr.format(ConfigHelper.GetConfigString("wxAppID",""), ConfigHelper.GetConfigString("wxAppSecret",""));
        }
    }
}