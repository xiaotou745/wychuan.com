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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Item(string id)
        {
            var blogsDTO = blogService.GetByBlogIdWithSections(id);

            return View(blogsDTO);
        }

        public ActionResult Category(string id)
        {
            ViewBag.Category = id;
            return View();
        }

        //public ActionResult PreView()
        //{
        //    var blog = DataCache.GetCache("preview_blog_key") as BlogsDTO;

        //    return View(blog);
        //}

        //public ActionResult Test(string id)
        //{
        //    Sections section = sectionService.GetBySectionId(id);
        //    if (section != null)
        //    {
        //        section.Childs = sectionService.Query(new SectionsQueryInfo {ParentId = section.Id});
        //    }
        //    return View(section);
        //}

        public ActionResult Demo()
        {
            return View();
        }
    }
}