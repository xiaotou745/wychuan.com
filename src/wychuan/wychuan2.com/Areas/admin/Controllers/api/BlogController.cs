using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AC.Extension;
using AC.Service.Blog;
using AC.Service.DTO.Blog;
using AC.Service.Impl.Blog;
using AC.Web;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers.api
{
    public class BlogController : ApiController
    {
        private readonly IBlogCategoryService blogCategoryService = new BlogCategoryService();
        private readonly IBlogTagsService blogTagsService = new BlogTagsService();

        #region BlogCategories
        [HttpPost]
        public AjaxResult SaveCategory(BlogCategoryDTO category)
        {
            if (category == null)
            {
                return AjaxResult.Error("请输入category");
            }
            if (category.Id == 0)
            {
                category.UserId = ApplicationUser.Current.UserId;
                category.Id = blogCategoryService.Create(category);
            }
            else
            {
                blogCategoryService.Rename(category.Id, category.Name);
            }
            return AjaxResult.Success(category.Id);
        }

        [HttpGet]
        [HttpPost]
        public AjaxResult RemoveCategory(int id)
        {
            if (id <= 0)
            {
                return AjaxResult.Error("id:{0}".format(id));
            }
            blogCategoryService.Remove(id);
            return AjaxResult.Success();
        }
        #endregion

        #region Blog Tags
        [HttpPost]
        public AjaxResult SaveTags(BlogTagsDTO tag)
        {
            if (tag == null)
            {
                return AjaxResult.Error("请输入tag");
            }
            if (tag.Id == 0)
            {
                tag.UserId = ApplicationUser.Current.UserId;
                tag.Id = blogTagsService.Create(tag);
            }
            else
            {
                //blogCategoryService.Rename(tag.Id, tag.Name);
            }
            return AjaxResult.Success(tag.Id);
        }
        #endregion
    }
}
