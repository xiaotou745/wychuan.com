using AC.Helper;

namespace wychuan2.com.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser
    {
        public static readonly ApplicationUser Empty = new ApplicationUser();
        private const string LOGIN_COOKIE_NAME = "loginuser";

        public static ApplicationUser Current
        {
            get
            {
                var applicationUser = CookieHelper.Get<ApplicationUser>(LOGIN_COOKIE_NAME);
                if (applicationUser == null)
                {
                    return Empty;
                }
                return applicationUser;
            }
        }

        public static void Login(ApplicationUser user)
        {
            CookieHelper.Set(LOGIN_COOKIE_NAME, user);
        }

        public static void LogOff()
        {
            CookieHelper.Remove(LOGIN_COOKIE_NAME);
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