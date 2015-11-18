using System.Collections.Generic;
using System.Web;
using AC.Service.DTO.User;
using AC.Service.Impl.Cache;
using AC.Util;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin
{
    public class AdminUtils
    {
        /// <summary>
        /// 获取所有菜单列表
        /// </summary>
        /// <returns></returns>
        public static IList<MenuDTO> GetMenus()
        {
            return UserCacheProvider.GetMenusInCache();
        }

        /// <summary>
        /// 获取当前登录用户的菜单权限
        /// </summary>
        /// <returns></returns>
        public static IList<int> GetUserPrivilege()
        {
            if (ApplicationUser.Current.Equals(ApplicationUser.Empty))
            {
                return null;
            }
            return UserCacheProvider.GetUserMenuIdsInCache(ApplicationUser.Current.UserId);
        }
        #region Route
        public static string CurrentController()
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }

        public static string CurrentAction()
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
                return (string)routeValues["action"];

            return string.Empty;
        }
        #endregion
        public static string LinkEmail
        {
            get { return ConfigUtils.GetConfigValue("LinkEmail","xiaotou745@gmail.com"); }
        }
    }
}