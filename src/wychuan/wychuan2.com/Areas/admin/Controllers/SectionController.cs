using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Service.Blog;
using AC.Service.DTO.Blog;
using AC.Service.Impl.Blog;
using AC.Web;
using wychuan2.com.Areas.admin.Models.Blog;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers
{
    public class SectionController : Controller
    {
        private readonly IBlogCategoryService blogCategoryService = new BlogCategoryService();
        private readonly IBlogTagsService blogTagsService = new BlogTagsService();

        private readonly ISectionsService sectionService = new SectionsService();

        #region Section Publish

        /// <summary>
        /// 发布段落
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public ActionResult Index(int? id, int? parentId)
        {
            var model = new SectionPublishModel();

            model.Categories = blogCategoryService.GetByUserId(ApplicationUser.Current.UserId);
            model.Tags = blogTagsService.GetByUserId(ApplicationUser.Current.UserId);
            if (id.HasValue)
            {
                Sections currentSection = sectionService.GetById(id.Value);
                model.CurrentId = id.Value;
                model.CurrentSection = currentSection;
                model.ParentId = currentSection.ParentId;
            }
            else if (parentId.HasValue)
            {
                Sections parentSection = sectionService.GetById(parentId.Value);
                model.ParentSection = parentSection;
                model.ParentId = parentId.Value;
                model.CurrentId = 0;
            }

            return View(model);
        }

        #endregion

        #region Save

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(Sections section)
        {
            if (section.Id == 0)
            {
                section.UserId = ApplicationUser.Current.UserId;
                section.Id = sectionService.Create(section);
            }
            else
            {
                sectionService.Modify(section);
            }

            return Json(AjaxResult.Success(section.Id));
        }

        #endregion

        #region 列表
        public ActionResult List()
        {
            var queryInfo = new SectionsQueryInfo {UserId = ApplicationUser.Current.UserId};

            IList<Sections> sectionses = sectionService.Query(queryInfo);

            SectionListModel model = new SectionListModel();
            model.Sections = sectionses;

            return View(model);
        }
        #endregion

        #region Remove

        [HttpGet]
        public AjaxResult Remove(int id)
        {
            sectionService.Remove(id);
            return AjaxResult.Success();
        }
        #endregion

        #region Refresh

        public ActionResult Refresh()
        {
            var queryInfo = new SectionsQueryInfo { UserId = ApplicationUser.Current.UserId };

            IList<Sections> sectionses = sectionService.Query(queryInfo);

            SectionListModel model = new SectionListModel();
            model.Sections = sectionses;

            return View("_SectionList", model);
        }
        #endregion
    }
}