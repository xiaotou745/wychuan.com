using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AC.Web.Common.Config.Menu
{
    public class AdminMenuConfig
    {
        /// <summary>
        /// 配置管理类
        /// </summary>
        private static ConfigsManager<List<MenuInfo>> configManager;

        private static List<MenuInfo> menus;

        private static string fileName;

        static AdminMenuConfig()
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
                    fileName = HttpContext.Current.Server.MapPath("~/Areas/admin/data/menuOfAdmin.xml");
                }
                else
                {
                    fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/menuOfAdmin.xml");
                }
            }
            return fileName;
        }
    }
}
