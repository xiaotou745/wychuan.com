using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AC.Helper;
using AC.Service.DTO.User;
using AC.Service.Impl.User;
using AC.Service.User;
using AC.Web;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers.api
{
    public class AccountController : ApiController
    {
        private readonly IUserService userService = new UserService();

        #region 登录
        public AjaxResult Logon(LogonAccount account)
        {
            if (account == null)
            {
                return AjaxResult.Error("用户名不允许为空.");
            }
            var user = new UserDTO()
            {
                LoginName = account.UserName,
                LoginPassword = account.Password,
            };
            LoginResult loginResult = userService.Login(user);
            if (loginResult == LoginResult.Success)
            {
                var applicationUser = new ApplicationUser()
                {
                    UserId = user.Id,
                    UserName = account.UserName,
                };
                ApplicationUser.Login(applicationUser);
                return AjaxResult.Success();
            }
            return AjaxResult.Error(EnumHelper.GetEnumDescription(loginResult));
        }
        #endregion

        #region 注册
        public AjaxResult Register(LogonAccount account)
        {
            if (account == null)
            {
                return AjaxResult.Error("用户名不允许为空.");
            }
            var user = new UserDTO {LoginName = account.UserName, LoginPassword = account.Password};
            RegisterResult registerResult = userService.Register(user);
            if (registerResult.Equals(RegisterResult.Success))
            {
                var applicationUser = new ApplicationUser()
                {
                    UserId = user.Id,
                    UserName = account.UserName,
                };
                ApplicationUser.Login(applicationUser);
                return AjaxResult.Success();
            }
            return AjaxResult.Error(EnumHelper.GetEnumDescription(registerResult));
        }
        #endregion

    }
}
