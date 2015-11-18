using AC.Helper;
using AC.Security;
using AC.Service.Impl.Cache;

namespace wychuan2.com.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser
    {
        public static readonly ApplicationUser Empty = new ApplicationUser();
        private static readonly string loginCookieName = MD5.Encrypt("loginuser");

        public static ApplicationUser Current
        {
            get
            {
                var applicationUser = CookieHelper.Get<ApplicationUser>(loginCookieName);
                if (applicationUser == null)
                {
                    return Empty;
                }
                return applicationUser;
            }
        }

        public static void Login(ApplicationUser user)
        {
            CookieHelper.Set(loginCookieName, user);
        }

        public static void LogOff()
        {
            if (!Current.Equals(Empty))
            {
                UserCacheProvider.ClearUserInCache(Current.UserId);
            }
            CookieHelper.Remove(loginCookieName);
        }

        public static bool IsLogin()
        {
            return Current.UserId > 0;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
    }

    public class LogonAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}