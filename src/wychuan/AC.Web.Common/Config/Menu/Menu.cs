using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using AC.Helper;

namespace AC.Web.Common.Config
{
    #region 菜单类型(系统)

    public enum MenuType
    {
        Admin,
        WeiXin
    }
    #endregion
    #region 菜单信息对象
    public class MenuInfo
    {
        /// <summary>
        /// 链接地址
        /// </summary>
        [XmlAttribute("href")]
        public string Href { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [XmlAttribute("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        [XmlAttribute("text")]
        public string Text { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        [XmlArray]
        public MenuInfo[] Childs { get; set; }
    }
    #endregion

#region 菜单服务

    public class MenuService
    {
        public static IList<MenuInfo> GetMenus(string menuPath)
        {
            var menuInfos = XmlHelper.Deserialize<MenuInfo[]>(menuPath);
            return menuInfos.ToList();
        }
    }
#endregion
}