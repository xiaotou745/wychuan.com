using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Service.DTO.LiCai;
using AC.Service.Impl.LiCai;
using AC.Service.LiCai;
using wychuan2.com.Areas.admin.Models.LiCai;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers.LiCai
{
    /// <summary>
    /// 账单
    /// </summary>
    public class BillController : BaseController
    {
        private readonly IAccountService accountService = new AccountService();
        private readonly IItemsService itemService = new ItemsService();
        private readonly ICategoryService categoryService = new CategoryService();

        // GET: licai/bill
        public ActionResult Index()
        {
            var model = new BillModel();
            
            model.Accounts = accountService.Query(new AccountQueryInfo {UserId = ApplicationUser.Current.UserId});
            model.Items = itemService.GetByUserId(ApplicationUser.Current.UserId);
            model.Categories = categoryService.GetByUserId(ApplicationUser.Current.UserId);

            return View(model);
        }

        public ActionResult Details()
        {
            var model = new BillModel();

            model.Accounts = accountService.Query(new AccountQueryInfo { UserId = ApplicationUser.Current.UserId });
            model.Items = itemService.GetByUserId(ApplicationUser.Current.UserId);
            model.Categories = categoryService.GetByUserId(ApplicationUser.Current.UserId);

            return View(model);
        }

    }
}
