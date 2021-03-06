﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers
{
    public class AccountController : Controller
    {
        #region 登录
        // GET: admin/Account/logon
        public ActionResult Logon()
        {
            if (ApplicationUser.IsLogin())
            {
                return Redirect("/admin");
            }
            return View();
        }
        #endregion

        #region 注册
        public ActionResult Register()
        {
            return View();
        }
        #endregion

        #region 注销
        public ActionResult LogOff()
        {
            ApplicationUser.LogOff();
            return Redirect("/admin/account/logon");
        }
        #endregion


        #region 忘记密码

        public ActionResult ForgetPassword()
        {
            return View();
        }
        #endregion
    }
}
