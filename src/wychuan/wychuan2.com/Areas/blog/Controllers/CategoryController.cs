using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wychuan2.com.Areas.blog.Controllers
{
    public class CategoryController : Controller
    {
        // GET: blog/Category
        public ActionResult Index(string id)
        {
            ViewBag.Category = id;
            return View();
        }
    }
}