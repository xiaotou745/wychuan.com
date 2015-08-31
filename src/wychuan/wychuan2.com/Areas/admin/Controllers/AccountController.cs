using System;
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
        // GET: admin/Account/logon
        public ActionResult Logon()
        {
            if (ApplicationUser.IsLogin())
            {
                return Redirect("/admin");
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            ApplicationUser.LogOff();
            return Redirect("/admin/account/logon");
        }
    }
}
