using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Extension;
using AC.Service;
using AC.Service.Blog;
using AC.Service.DTO;
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
        private readonly ISectionAnchorsService anchorService = new SectionAnchorsService();

        #region Childs

        public AjaxResult SaveChilds(Sections section)
        {
            sectionService.SaveChilds(section);
            return AjaxResult.Success();
        }
        #endregion

        #region Section Publish

        /// <summary>
        /// 发布段落
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public ActionResult Index(int? id)
        {
            var model = new SectionPublishModel();

            model.Categories = blogCategoryService.GetByUserId(ApplicationUser.Current.UserId);
            model.Tags = blogTagsService.GetByUserId(ApplicationUser.Current.UserId);

            if (id.HasValue) //编辑
            {
                model.CurrentSection = sectionService.GetById(id.Value, true, true, true);
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
            try
            {
                if (section.Id == 0)
                {
                    section.Id = sectionService.Create(section);
                }
                else
                {
                    sectionService.Modify(section);
                }
            }
            catch (ServiceException exception)
            {
                return Json(AjaxResult.Error(exception.Message));
            }
            catch (Exception ex)
            {
                throw ex;
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
                //ParentId = 0,
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
            model.ParentId = queryInfo.ParentId ?? 0;
            model.IsParents = queryInfo.IsParents;
            model.PagedSections = sectionService.QueryPaged(queryInfo);

            return View("_SectionList", model);
        }
        #endregion

        #region Anchors
        #region Save Anchor

        public AjaxResult SaveAnchor(SectionAnchors anchor)
        {
            if (anchor == null)
            {
                return AjaxResult.Error("参数为空.");
            }
            if (anchor.Id == 0)
            {
                anchor.Id = anchorService.Create(anchor);
            }
            else
            {
                anchorService.Modify(anchor);
            }
            return AjaxResult.Success(anchor.Id);
        } 
        #endregion

        #region Refresh Anchors

        public ActionResult RefreshAnchors(int sectionId)
        {
            IList<SectionAnchors> anchors = anchorService.GetBySectionId(sectionId);

            SectionPublishModel model = new SectionPublishModel(){CurrentSection = new Sections()};
            model.CurrentSection.Anchors = anchors;

            return View("_SectionAnchorsList", model);
        }

        #endregion

        #region Remove Anchor

        public AjaxResult RemoveAnchor(int id)
        {
            anchorService.Remove(id);
            return AjaxResult.Success();
        }
        #endregion
        #endregion
    }
}