using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Service.DTO.LiCai;
using AC.Service.Impl.LiCai;
using AC.Service.LiCai;
using AC.Web;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers.LiCai
{
    /// <summary>
    /// 理财账户管理
    /// </summary>
    public class AccountController : BaseController
    {
        /// <summary>
        /// 理财账户Service
        /// </summary>
        private readonly IAccountService accountService = new AccountService();

        #region 列表

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="type">账户类型</param>
        /// <returns></returns>
        public ActionResult Index(int type = 0)
        {
            var model = new LiCaiAccount();

            List<AccountType> accountTypes =
                AccountType.Create(Server.MapPath("~/data/lc_accounttype.config"), true).GetAccountTypes();

            //账户类型s
            model.AccountTypes = accountTypes;

            Dictionary<int, int> typeCounts = accountService.GetTypeCounts();
            accountTypes.ForEach(t => t.Count = typeCounts.ContainsKey(t.Id) ? typeCounts[t.Id] : 0);

            //当前类型
            model.CurrentType = accountTypes.FirstOrDefault(a => a.Id == type);

            var queryInfo = new AccountQueryInfo {UserId = ApplicationUser.Current.UserId};
            if (type > 0)
            {
                queryInfo.AccountTypeId = type;
            }
            //查询账户列表
            IList<AccountDTO> lstAccounts = accountService.Query(queryInfo);
            model.Accounts = lstAccounts;
            return View(model);
        }

        #endregion

        #region 保存账户

        [HttpPost]
        public AjaxResult Save(AccountDTO account)
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

        #endregion

        #region 删除账户
        // POST: licai/Account/Delete/5
        [HttpPost]
        public AjaxResult Delete(int id)
        {
            accountService.Remove(id);

            return AjaxResult.Success();
        }
        #endregion
    }
}