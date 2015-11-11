using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Cache;
using AC.Service.Blog;
using AC.Service.DTO.Blog;
using AC.Service.Impl.Blog;

namespace wychuan2.com.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogsService blogService = new BlogsService();
        // GET: Blog
        public ActionResult Index(int? id)
        {
            if (id.HasValue && id != 0)
            {
                return View("BlogItem");
            }
            return View();
        }

        public ActionResult Item(int id)
        {
            var blogsDTO = blogService.GetById(id);

            return View(blogsDTO);
        }

        public ActionResult Category(string id)
        {
            ViewBag.Category = id;
            return View();
        }

        public ActionResult PreView()
        {
            var blog = DataCache.GetCache("preview_blog_key") as BlogsDTO;

            return View(blog);
        }
    }
}