using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Service.DTO.LiCai;
using AC.Service.Impl.LiCai;
using AC.Service.LiCai;
using AC.Web;
using wychuan2.com.Areas.admin.Models.LiCai;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers
{
    public class LiCaiController : BaseController
    {
        private readonly IItemsService itemService = new ItemsService();
        private readonly ICategoryService categoryService = new CategoryService();
        private readonly IBillTemplateService billTemplateService = new BillTemplateService();

        #region 账户
        /// <summary>
        /// 理财账户Service
        /// </summary>
        private readonly IAccountService accountService = new AccountService();
        public ActionResult Account(int type = 0)
        {
            var model = new LiCaiAccount();

            List<AccountType> accountTypes = AccountType.Create().GetAccountTypes();

            //账户类型s
            model.AccountTypes = accountTypes;

            Dictionary<int, int> typeCounts = accountService.GetTypeCounts(ApplicationUser.Current.UserId);
            accountTypes.ForEach(t => t.Count = typeCounts.ContainsKey(t.Id) ? typeCounts[t.Id] : 0);

            //当前类型
            model.CurrentType = accountTypes.FirstOrDefault(a => a.Id == type);

            var queryInfo = new AccountQueryInfo { UserId = ApplicationUser.Current.UserId };
            if (type > 0)
            {
                queryInfo.AccountTypeId = type;
            }
            //查询账户列表
            IList<AccountDTO> lstAccounts = accountService.Query(queryInfo);
            model.Accounts = lstAccounts;
            return View(model);
        }

        [HttpPost]
        public AjaxResult SaveAccount(AccountDTO account)
        {
            if (!ApplicationUser.IsLogin())
            {
                Redirect("/admin");
                return null;
            }
            account.UserId = ApplicationUser.Current.UserId;
            if (account.Id == 0)
            {
                account.CreateBy = ApplicationUser.Current.UserName;

                accountService.Create(account);
            }
            else
            {
                accountService.Modify(account);
            }

            return AjaxResult.Success();
        }

        [HttpPost]
        public AjaxResult DeleteAccount(int id)
        {
            accountService.Remove(id);

            return AjaxResult.Success();
        }

        public ActionResult AccountList(int type = 0)
        {
            var model = new LiCaiAccount();

            List<AccountType> accountTypes = AccountType.Create().GetAccountTypes();

            //账户类型s
            model.AccountTypes = accountTypes;

            Dictionary<int, int> typeCounts = accountService.GetTypeCounts(ApplicationUser.Current.UserId);
            accountTypes.ForEach(t => t.Count = typeCounts.ContainsKey(t.Id) ? typeCounts[t.Id] : 0);

            //当前类型
            model.CurrentType = accountTypes.FirstOrDefault(a => a.Id == type);

            var queryInfo = new AccountQueryInfo { UserId = ApplicationUser.Current.UserId };
            if (type > 0)
            {
                queryInfo.AccountTypeId = type;
            }
            //查询账户列表
            IList<AccountDTO> lstAccounts = accountService.Query(queryInfo);
            model.Accounts = lstAccounts;
            return View("_AccountList", model);
        }
        #endregion

        #region 设置
        public ActionResult Setting()
        {
            IList<ItemDTO> lstItems = itemService.GetByUserId(ApplicationUser.Current.UserId);
            ViewBag.Items = lstItems;

            IList<CategoryDTO> lstCategories = categoryService.GetByUserId(ApplicationUser.Current.UserId);
            ViewBag.Categories = lstCategories;

            return View();
        }
        #endregion

        #region 记账

        public ActionResult Bill()
        {
            var model = new BillModel();

            model.Accounts = accountService.Query(new AccountQueryInfo { UserId = ApplicationUser.Current.UserId });
            model.Items = itemService.GetByUserId(ApplicationUser.Current.UserId);
            model.Categories = categoryService.GetByUserId(ApplicationUser.Current.UserId);

            return View(model);
        }
        #endregion

        #region 快速记账

        public ActionResult FastBill()
        {
            var model = new BillModel();

            model.Accounts = accountService.Query(new AccountQueryInfo { UserId = ApplicationUser.Current.UserId });
            model.Items = itemService.GetByUserId(ApplicationUser.Current.UserId);
            model.Categories = categoryService.GetByUserId(ApplicationUser.Current.UserId);
            model.Templates = billTemplateService.GetByUserId(ApplicationUser.Current.UserId);

            return View(model);
        }

        public ActionResult RefreshBill()
        {
            var model = new BillModel();
            IList<BillTemplateDTO> lstTemplates = billTemplateService.GetByUserId(ApplicationUser.Current.UserId);
            model.Templates = lstTemplates;

            return View("_FastBillList", model);
        }
        #endregion

        #region 明细

        public ActionResult Details()
        {
            var model = new BillModel();

            model.Accounts = accountService.Query(new AccountQueryInfo { UserId = ApplicationUser.Current.UserId });
            model.Items = itemService.GetByUserId(ApplicationUser.Current.UserId);
            model.Categories = categoryService.GetByUserId(ApplicationUser.Current.UserId);

            return View(model);
        }
        #endregion

        #region 预算

        public ActionResult YuSuan()
        {
            return View();
        }
        #endregion

        #region 统计

        public ActionResult TongJi()
        {
            IList<ItemDTO> lstItems = itemService.GetByUserId(ApplicationUser.Current.UserId);
            ViewBag.Items = lstItems;

            IList<CategoryDTO> lstCategories = categoryService.GetByUserId(ApplicationUser.Current.UserId);
            ViewBag.Categories = lstCategories;

            return View();
        }
        #endregion
    }
}
