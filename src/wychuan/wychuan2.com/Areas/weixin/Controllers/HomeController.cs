using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Web.Common.Config;
using wychuan2.com.App_Start;
using wychuan2.com.Areas.admin.Controllers;

namespace wychuan2.com.Areas.weixin.Controllers
{
    //[BasicAuth]
    //[Authorize]
    public class HomeController : BaseController
    {
        // GET: weixin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
