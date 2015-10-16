using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AC.Extension;
using AC.Service.DTO.LiCai;
using AC.Service.Impl.LiCai;
using AC.Service.LiCai;
using AC.Util;
using AC.Web;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers.api
{
    public class ItemsController : ApiController
    {
        private readonly ICategoryService categoryService = new CategoryService();
        private readonly IItemsService itemsService = new ItemsService();

        #region Categories

        /// <summary>
        /// /api/items/savecategory
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult SaveCategory(CategoryDTO category)
        {
            if (category == null)
            {
                return AjaxResult.Error("请输入category");
            }
            if (category.Id == 0)
            {
                category.UserId = ApplicationUser.Current.UserId;
                category.Id = categoryService.Create(category);
            }
            else
            {
                categoryService.Rename(category.Id, category.Name);
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
            categoryService.Remove(id);
            return AjaxResult.Success();
        }

        #endregion

        #region Items

        [HttpPost]
        [HttpGet]
        public AjaxResult SaveItems(ItemDTO item)
        {
            if (item == null)
            {
                return AjaxResult.Error("请输入item");
            }
            if (item.Id <= 0)
            {
                item.UserId = ApplicationUser.Current.UserId;
                item.Level = 1000;
                item.Id = itemsService.Create(item);
            }
            else
            {
                itemsService.Rename(item.Id, item.Name);
            }
            return AjaxResult.Success(item.Id);
        }

        [HttpGet]
        [HttpPost]
        public AjaxResult RemoveItem(int id)
        {
            if (id <= 0)
            {
                return AjaxResult.Error("id:{0}".format(id));
            }
            itemsService.Remove(id);
            return AjaxResult.Success();
        }

        #endregion
    }
}