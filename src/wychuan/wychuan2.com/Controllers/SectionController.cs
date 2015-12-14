using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Service.Blog;
using AC.Service.DTO.Blog;
using AC.Service.Impl.Blog;

namespace wychuan2.com.Controllers
{
    public class SectionController : Controller
    {
        private readonly ISectionsService sectionService = new SectionsService();
        // GET: Section
        public ActionResult Index(string id)
        {
            Sections section = sectionService.GetBySectionId(id);
            if (section != null)
            {
                section.Childs = sectionService.Query(new SectionsQueryInfo { ParentId = section.Id });
            }
            return View(section);
        }

        public ActionResult Preview(string id)
        {
            Sections section = sectionService.GetBySectionId(id);
            if (section != null)
            {
                section.Childs = sectionService.Query(new SectionsQueryInfo {ParentId = section.Id});
            }
            return View(section);
        }
    }
}