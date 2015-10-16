using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Service.DTO.LiCai;
using AC.Service.Impl.LiCai;
using AC.Service.LiCai;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers.LiCai
{
    public class SettingController : BaseController
    {
        private readonly IItemsService itemService = new ItemsService();
        private readonly ICategoryService categoryService = new CategoryService();
        // GET: admin/Setting
        public ActionResult Index()
        {
            IList<ItemDTO> lstItems = itemService.GetByUserId(ApplicationUser.Current.UserId);
            ViewBag.Items = lstItems;

            IList<CategoryDTO> lstCategories = categoryService.GetByUserId(ApplicationUser.Current.UserId);
            ViewBag.Categories = lstCategories;

            return View();
        }
    }
}
