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
        private readonly IBillService billService = new BillService();
        private readonly IAccountService accountService = new AccountService();

        #region 账户
        /// <summary>
        /// 理财账户Service
        /// </summary>
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
            IList<AccountDTO> lstAccounts = accountService.Search(queryInfo);
            model.Accounts = lstAccounts;
            return View(model);
        }
        #endregion
        #region 账户2
        /// <summary>
        /// 理财账户Service
        /// </summary>
        public ActionResult Account2(int type = 0)
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
            IList<AccountDTO> lstAccounts = accountService.Search(queryInfo);
            model.Accounts = lstAccounts;
            return View(model);
        }
        [HttpPost]
        public AjaxResult SaveAccount(AccountDTO account)
        {
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

        public ActionResult AccountDetails(int type = 0)
        {
            var model = new LiCaiAccount();

            List<AccountType> accountTypes = AccountType.Create().GetAccountTypes();

            //账户类型s
            model.AccountTypes = accountTypes;

            Dictionary<int, int> typeCounts = accountService.GetTypeCounts(ApplicationUser.Current.UserId);
            accountTypes.ForEach(t => t.Count = typeCounts.ContainsKey(t.Id) ? typeCounts[t.Id] : 0);

            //当前类型
            model.CurrentType = accountTypes.FirstOrDefault(a => a.Id == type);

            var queryInfo = new AccountQueryInfo
            {
                UserId = ApplicationUser.Current.UserId,IsEnable=null
            };
            if (type > 0)
            {
                queryInfo.AccountTypeId = type;
            }
            //查询账户列表
            IList<AccountDTO> lstAccounts = accountService.Search(queryInfo);
            model.Accounts = lstAccounts;
            return View("_AccountDetails", model);
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
            IList<AccountDTO> lstAccounts = accountService.Search(queryInfo);
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

        public ActionResult Details(int? id)
        {
            var model = new BillModel();

            model.CurrentAccountId = id ?? 0;
            model.Accounts = accountService.Query(new AccountQueryInfo { UserId = ApplicationUser.Current.UserId,IsEnable=null });
            model.Items = itemService.GetByUserId(ApplicationUser.Current.UserId);
            model.Categories = categoryService.GetByUserId(ApplicationUser.Current.UserId);
            model.Details =
                billService.Query(new BillQueryInfo()
                {
                    AccountId = id??0,
                    StartTime = DateTime.Now.Date.AddDays(-30),
                    EndTime = DateTime.Now.AddDays(1).Date,
                    UserId = ApplicationUser.Current.UserId
                });
            model.DetailType = 0;//全部

            if (id.HasValue && id.Value > 0)
            {
                return View("DetailsOfAccount", model);
            }
            return View(model);
        }

        public ActionResult QueryDetails(BillQueryInfo queryInfo)
        {
            var model = new BillModel();

            if (queryInfo == null)
            {
                queryInfo = new BillQueryInfo();
            }
            if (queryInfo.TimeZone == 1)
            {
                queryInfo.EndTime = DateTime.Now.AddDays(1).Date;
                queryInfo.StartTime = DateTime.Now.AddDays(-7).Date;
            }
            else if (queryInfo.TimeZone == 2)
            {
                queryInfo.EndTime = DateTime.Now.AddDays(1).Date;
                queryInfo.StartTime = DateTime.Now.AddDays(-30).Date;
            }
            else if(queryInfo.EndTime.HasValue)
            {
                queryInfo.EndTime = queryInfo.EndTime.Value.AddDays(1).Date;
            }
            queryInfo.UserId = ApplicationUser.Current.UserId;

            model.Details = billService.Query(queryInfo);
            model.Accounts = accountService.Query(new AccountQueryInfo { UserId = ApplicationUser.Current.UserId, IsEnable = null });
            model.Items = itemService.GetByUserId(ApplicationUser.Current.UserId);
            model.Categories = categoryService.GetByUserId(ApplicationUser.Current.UserId);
            model.DetailType = queryInfo.DetailType;

            return View("_BillList", model);
        }

        public ActionResult BillItem(int id)
        {
            var model = new BillModel
            {
                Accounts = accountService.Query(new AccountQueryInfo { UserId = ApplicationUser.Current.UserId, IsEnable = null }),
                Items = itemService.GetByUserId(ApplicationUser.Current.UserId),
                Categories = categoryService.GetByUserId(ApplicationUser.Current.UserId),
                Details = new List<BillDTO> {billService.GetById(id)}
            };

            return View("_BillItem", model);
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
            var model = new BillModel();

            model.Accounts = accountService.Query(new AccountQueryInfo { UserId = ApplicationUser.Current.UserId });
            model.Items = itemService.GetByUserId(ApplicationUser.Current.UserId);
            model.Categories = categoryService.GetByUserId(ApplicationUser.Current.UserId);
            model.Details =
                billService.Query(new BillQueryInfo()
                {
                    StartTime = DateTime.Now.Date.AddDays(-7),
                    EndTime = DateTime.Now,
                    UserId = ApplicationUser.Current.UserId
                });

            return View(model);
        }
        #endregion

        #region P2P理财

        private readonly ILiCaiDetailService liCaiService = new LiCaiDetailService();

        public ActionResult P2P()
        {
            var model = new LiCaiModel();

            model.Accounts = accountService.Query(new AccountQueryInfo { UserId = ApplicationUser.Current.UserId });
            model.LiCaiDetails = liCaiService.GetByUserId(ApplicationUser.Current.UserId);
            
            return View(model);
        }

        public ActionResult CreateP2p(LiCaiDetailsDTO detail)
        {
            detail.UserId = ApplicationUser.Current.UserId;
            if (detail.Id == 0)
            {
                detail.Id = liCaiService.Create(detail);
            }
            else
            {
                liCaiService.Modify(detail);
            }
            var lstDetails = liCaiService.GetByUserId(ApplicationUser.Current.UserId);
            var model = new LiCaiModel()
            {
                LiCaiDetails = lstDetails,
            };
            return View("_P2PList", model);
        }

        public ActionResult DeleteP2p(int id)
        {
            liCaiService.Remove(id);
            var lstDetails = liCaiService.GetByUserId(ApplicationUser.Current.UserId);
            var model = new LiCaiModel()
            {
                LiCaiDetails = lstDetails,
            };
            return View("_P2PList", model);
        }

        public ActionResult GetLiCaiById(int id)
        {
            var model = new LiCaiModel();
            var liCaiDetailsDTO = liCaiService.GetById(id);

            model.Accounts = accountService.Query(new AccountQueryInfo { UserId = ApplicationUser.Current.UserId });
            model.LiCaiDetails = new List<LiCaiDetailsDTO>() { liCaiDetailsDTO };

            return View("_ModalP2PItem", model);
        }

        #endregion
    }
}
