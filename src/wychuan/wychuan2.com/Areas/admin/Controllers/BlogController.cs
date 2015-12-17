using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AC.Cache;
using AC.Extension;
using AC.Page;
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
    public class BlogController : BaseController
    {
        private readonly IBlogCategoryService blogCategoryService = new BlogCategoryService();
        private readonly IBlogTagsService blogTagsService = new BlogTagsService();
        private readonly IBlogsService blogService = new BlogsService();
        private readonly ISectionsService sectionService = new SectionsService();

        #region QueryBlogSections

        public ActionResult SearchSections(SectionsQueryInfo queryInfo)
        {
            if (queryInfo == null)
            {
                return View("_BlogSectionList", null);
            }
            queryInfo.UserId = ApplicationUser.Current.UserId;
            queryInfo.ParentId = 0;
            if (!string.IsNullOrEmpty(queryInfo.StrTagIds))
            {
                queryInfo.TagIds = queryInfo.StrTagIds.ToNumberList();
            }
            IList<Sections> allSections = sectionService.Query(queryInfo);
            List<int> checkedSectionIds = queryInfo.CheckedSectionIds ?? new List<int>();
            List<Sections> model = allSections.Where(s => !checkedSectionIds.Contains(s.Id)).Take(20).ToList();
            return View("_BlogSectionList", model);
        }
        #endregion

        #region 创建随笔

        // GET: admin/Blog
        public ActionResult Index(int? id)
        {
            var model = new BlogModel();

            model.Categories = blogCategoryService.GetByUserId(ApplicationUser.Current.UserId);
            model.Tags = blogTagsService.GetByUserId(ApplicationUser.Current.UserId);

            var lstSectionIds = new List<string>();
            if (id.HasValue)
            {
                BlogsDTO currentBlogs = blogService.GetById(id.Value);
                if (string.IsNullOrEmpty(currentBlogs.SectionIds))
                {
                    currentBlogs.BlogSections = new List<Sections>();
                }
                else
                {
                    lstSectionIds =
                        currentBlogs.SectionIds.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries).ToList();
                    currentBlogs.BlogSections = sectionService.Query(new SectionsQueryInfo {SectionIds = lstSectionIds});
                }
                model.CurrentBlogs = currentBlogs;
            }

            var queryInfo = new SectionsQueryInfo {UserId = ApplicationUser.Current.UserId, ParentId = 0};
            IList<Sections> allSections = sectionService.Query(queryInfo);
            model.SectionsUnChecked = allSections.Where(s => !lstSectionIds.Contains(s.SectionId)).Take(20).ToList();

            return View(model);
        }

        public ActionResult UploadFile()
        {
            var fileCount = Request.Files.Count;
            if (fileCount == 0)
            {
                return Json(AjaxResult.Error());
            }
            string uppath = string.Empty;
            string savepath = string.Empty;
            var imgFile = Request.Files[0];
            //创建图片新的名称
            string nameImg = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            //获得上传图片的文件名
            string strPath = imgFile.FileName;
            //获得上传图片的类型(后缀名)
            string type = strPath.Substring(strPath.LastIndexOf(".") + 1).ToLower();

            //拼写上传图片的路径
            uppath = Server.MapPath("~/UpImgs/");
            if (!Directory.Exists(uppath))
            {
                Directory.CreateDirectory(uppath);
            }
            uppath += nameImg + "." + type;
            string urlPath = Request.Url.Host+ "/upimgs/" + nameImg + "." + type;
            //上传图片
            imgFile.SaveAs(uppath);
            return Json(AjaxResult.Success(urlPath));
        }

        #endregion

        #region 我的随笔列表

        public ActionResult List()
        {
            var model = new BlogListModel();

            model.Categories = blogCategoryService.GetByUserId(ApplicationUser.Current.UserId);
            model.Tags = blogTagsService.GetByUserId(ApplicationUser.Current.UserId);

            BlogsQueryInfo queryInfo = new BlogsQueryInfo
            {
                PageIndex=1,
                UserId = ApplicationUser.Current.UserId
            };
            IPagedList<BlogsDTO> blogs = blogService.QueryPaged(queryInfo);
            model.BlogsPaged = blogs;

            return View(model);
        }

        #endregion

        public ActionResult Search(BlogsQueryInfo queryInfo)
        {
            var model = new BlogListModel();
            queryInfo.UserId = ApplicationUser.Current.UserId;
            IPagedList<BlogsDTO> blogs = blogService.QueryPaged(queryInfo);
            model.BlogsPaged = blogs;
            return View("_BlogList", model);
        }

        #region 设置

        public ActionResult Setting()
        {
            var model = new BlogSettingModel();

            model.Categories = blogCategoryService.GetByUserId(ApplicationUser.Current.UserId);
            model.Tags = blogTagsService.GetByUserId(ApplicationUser.Current.UserId);

            return View(model);
        }

        #endregion

        #region 预览

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PreView(BlogsDTO blog)
        {
            blog.PostTime = DateTime.Now;
            blog.AuthorId = ApplicationUser.Current.UserId;
            blog.Author = ApplicationUser.Current.UserName;

            DataCache.SetCache("preview_blog_key", blog);

            return Json(AjaxResult.Success());
        }
        #endregion

        public ActionResult Pre(int id)
        {
            BlogsDTO currentBlog = blogService.GetByIdWithSections(id);
            return View(currentBlog);
        }

        public ActionResult GetBlogPre(int id, string sectionIds)
        {
            BlogsDTO currentBlog = blogService.GetByIdWithSections(id, sectionIds);
            return View("Pre", currentBlog);
        }

        public AjaxResult Remove(int id)
        {
            blogService.Remove(id);
            return AjaxResult.Success();
        }
        
        #region Save
        [HttpPost]
        public ActionResult Save(BlogsDTO blog)
        {
            blog.AuthorId = ApplicationUser.Current.UserId;
            if (blog.Id == 0)
            {
                blog.Author = ApplicationUser.Current.UserName;

                blog.Id = blogService.Create(blog);
            }
            else
            {
                blogService.Modify(blog);
            }

            return Json(AjaxResult.Success(blog.Id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveSections(BlogsDTO blog)
        {
            AppLogger.LoggerOfWeiXin.Info(JsonHelper.ToJson(blog));
            blogService.ModifySections(blog);

            return Json(AjaxResult.Success());
        }
        #endregion
    }
}