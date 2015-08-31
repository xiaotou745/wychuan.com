using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Extension;
using AC.Helper;

namespace AC.Service.WeiXin.Common
{
    public class WeiXinUrl
    {
        private static string wxtoken = ConfigHelper.GetConfigString("wxtoken", string.Empty);
        private static string wxAppID = ConfigHelper.GetConfigString("wxAppID", string.Empty);
        private static string wxAppSecret = ConfigHelper.GetConfigString("wxAppSecret", string.Empty);

        /// <summary>
        /// 获取accesstoken接口地址
        /// </summary>
        /// <returns></returns>
        public static string AccessTokenUrl
        {
            get
            {
                return @"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}"
                    .format(wxAppID, wxAppSecret);
            }
        }

        #region Menu Urls

        /// <summary>
        /// 自定义菜单创建接口地址
        /// </summary>
        public static string MenuCreateUrl
        {
            get
            {
                return
                    @"https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}".format(
                        AccessTokenService.GetAccessToken());
            }
        }
        /// <summary>
        /// 自定义菜单查询接口地址
        /// </summary>
        public static string MenuGetUrl
        {
            get
            {
                return
                    @"https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}".format(
                        AccessTokenService.GetAccessToken());
            }
        }

        /// <summary>
        /// 自定义菜单删除接口URL
        /// </summary>
        public static string MenuDeleteUrl
        {
            get
            {
                return
                    @"https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}".format(
                        AccessTokenService.GetAccessToken());
            }
        }

        /// <summary>
        /// 获取自定义菜单配置接口URL
        /// </summary>
        public static string MenuSelfInfoUrl
        {
            get
            {
                return
                    @"https://api.weixin.qq.com/cgi-bin/get_current_selfmenu_info?access_token={0}".format(
                        AccessTokenService.GetAccessToken());
            }
        }
        #endregion
    }
}
