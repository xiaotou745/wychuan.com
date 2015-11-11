using System;
using System.IO;
using System.Web.Mvc;
using AC.Cache;
using AC.Service.Blog;
using AC.Service.DTO.Blog;
using AC.Service.Impl.Blog;
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

        #region 创建随笔

        // GET: admin/Blog
        public ActionResult Index()
        {
            var model = new BlogModel();

            model.Categories = blogCategoryService.GetByUserId(ApplicationUser.Current.UserId);
            model.Tags = blogTagsService.GetByUserId(ApplicationUser.Current.UserId);

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
            var model = new BlogModel();

            model.Categories = blogCategoryService.GetByUserId(ApplicationUser.Current.UserId);
            model.Tags = blogTagsService.GetByUserId(ApplicationUser.Current.UserId);

            return View(model);
        }

        #endregion

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

        #region Save
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(BlogsDTO blog)
        {
            if (blog.Id == 0)
            {
                blog.PostTime = DateTime.Now;
                blog.AuthorId = ApplicationUser.Current.UserId;
                blog.Author = ApplicationUser.Current.UserName;

                blog.Id = blogService.Create(blog);
            }

            return Json(AjaxResult.Success(blog.Id));
        }
        #endregion
    }
}