using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Service;
using AC.Service.Blog;
using AC.Service.DTO.Blog;
using AC.Service.Impl.Blog;
using AC.Util;
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

            if (id.HasValue) //编辑
            {
                model.CurrentSection = sectionService.GetById(id.Value);
                if (model.CurrentSection.ParentId == 0)
                {
                    model.Operate = SectionOperate.ModifyParent;
                }
                else
                {
                    model.Operate = SectionOperate.ModifyChild;
                }
            }
            else if (parentId.HasValue) //新增子段落
            {
                model.ParentSection = sectionService.GetById(parentId.Value);
                model.Operate = SectionOperate.CreateChild;
            }
            else //新增父段落
            {
                model.Operate = SectionOperate.CreateNew;
            }

            return View(model);
        }

        #endregion

        #region Save

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(Sections section)
        {
            section.UserId = ApplicationUser.Current.UserId;
            if (section.Id == 0)
            {
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
            var queryInfo = new SectionsQueryInfo
            {
                UserId = ApplicationUser.Current.UserId,
                PageIndex = 1,
                ParentId = 0,
            };

            //IList<Sections> sectionses = sectionService.Query(queryInfo);

            SectionListModel model = new SectionListModel();
            model.Categories = blogCategoryService.GetByUserId(ApplicationUser.Current.UserId);
            model.Tags = blogTagsService.GetByUserId(ApplicationUser.Current.UserId);

            //model.Sections = sectionses;
            model.IsParents = true;
            model.PagedSections = sectionService.QueryPaged(queryInfo);

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

        public ActionResult Refresh(SectionsQueryInfo queryInfo)
        {
            if (queryInfo == null)
            {
                return null;
            }
            queryInfo.UserId = ApplicationUser.Current.UserId;

            var model = new SectionListModel();
            model.IsParents = queryInfo.ParentId == 0 ? true : false;
            model.PagedSections = sectionService.QueryPaged(queryInfo);

            return View("_SectionList", model);
        }
        #endregion
    }
}