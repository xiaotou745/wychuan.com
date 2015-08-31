using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace AC.Web.Common.Config.Menu
{
    public class WeixinMenuConfig
    {
        /// <summary>
        /// 配置管理类
        /// </summary>
        private static ConfigsManager<List<MenuInfo>> configManager;

        private static List<MenuInfo> menus;

        private static string fileName;

        static WeixinMenuConfig()
        {
            if (configManager == null)
            {
                configManager = new ConfigsManager<List<MenuInfo>>(GetFilePath());
                menus = configManager.Get();
            }
        }

        public static List<MenuInfo> Get()
        {
            return menus;
        }

        public static void Save()
        {
            configManager.Save(menus);
        }
        
        private static string GetFilePath()
        {
            if (string.IsNullOrEmpty(fileName))
            {
                if (!Equals(HttpContext.Current, null))
                {
                    fileName = HttpContext.Current.Server.MapPath("~/Areas/weixin/data/menuOfWeixin.xml");
                }
                else
                {
                    fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Areas/weixin/data/menuOfWeixin.xml");
                }
            }
            return fileName;
        }
    }
}
