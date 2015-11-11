using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wychuan2.com.Areas.blog.Controllers
{
    public class HomeController : Controller
    {
        // GET: blog/Home
        public ActionResult Index(int? id)
        {
            if (id.HasValue && id != 0)
            {
                return View("BlogItem");
            }
            return View();
        }
    }
}