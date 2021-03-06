﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AC.Helper;
using AC.Service.DTO.User;
using AC.Service.Impl.LiCai;
using AC.Service.Impl.User;
using AC.Service.LiCai;
using AC.Service.User;
using AC.Web;
using wychuan2.com.Areas.admin.Models.User;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers.api
{
    public class AccountController : ApiController
    {
        private readonly IUserService userService = new UserService();
        private readonly ICategoryService categoryService = new CategoryService();
        private readonly IRoleService roleService = new RoleService();

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
            RegisterResult registerResult = userService.Register(user); //调用注册接口
            if (registerResult.Equals(RegisterResult.Success))
            {
                //如果注册成功,操作登录;
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

        #region Remove

        [HttpGet]
        [HttpPost]
        public AjaxResult Remove(int userId)
        {
            if (userId <= 0)
            {
                return AjaxResult.Error();
            }
            userService.Remove(userId);
            return AjaxResult.Success();
        }

        #endregion

        #region Disable

        [HttpGet]
        [HttpPost]
        public AjaxResult Disable(int userId, bool disable)
        {
            if (userId <= 0)
            {
                return AjaxResult.Error("userId is <=0");
            }
            userService.Disable(userId, disable);
            return AjaxResult.Success();
        }

        #endregion

        #region SetRoles
        
        public AjaxResult SetRoles(UserRoleParams userRoleParams)
        {
            userService.SetRoles(userRoleParams.UserId, userRoleParams.RoleIds);
            return AjaxResult.Success();
        }
        #endregion

        #region CreateRole

        public AjaxResult SaveRole(RoleDTO role)
        {
            if (role == null)
            {
                return AjaxResult.Error("role is null");
            }
            if (role.Id == 0)
            {
                role.Id = roleService.Create(role);
            }
            else
            {
                roleService.Modify(role);
            }
            return AjaxResult.Success(role.Id);
        }

        #endregion

        #region Remove Role

        [HttpGet]
        [HttpPost]
        public AjaxResult RemoveRole(int roleId)
        {
            if (roleId <= 0)
            {
                return AjaxResult.Error();
            }
            roleService.Remove(roleId);
            return AjaxResult.Success();
        }

        #endregion
    }
}