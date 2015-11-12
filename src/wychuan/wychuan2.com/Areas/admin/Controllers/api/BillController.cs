using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AC.Service.DTO.LiCai;
using AC.Service.Impl.LiCai;
using AC.Service.LiCai;
using AC.Web;
using wychuan2.com.Areas.admin.Models.LiCai;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers.api
{
    public class BillController : ApiController
    {
        private readonly ICategoryService categoryService = new CategoryService();
        private readonly IAccountService accountService = new AccountService();
        private readonly IBillService billService = new BillService();
        private readonly IBillTemplateService billTemplateService = new BillTemplateService();

        #region bill
        public AjaxResult Save(BillDTO bill)
        {
            if (bill == null)
            {
                return AjaxResult.Error("bill is null");
            }
            if (bill.ID == 0)
            {
                bill.UserId = ApplicationUser.Current.UserId;
                bill.ID = billService.Create(bill); //添加账单流水
            }
            else
            {
                billService.Modify(bill);
            }

            return AjaxResult.Success(bill.ID);
        }

        /// <summary>
        /// 快速保存，根据模板保存的支出记录
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult FastSave(BillDTO bill)
        {
            bill.UserId = ApplicationUser.Current.UserId;
            bill.DetailType = BillDetailType.Expend.GetHashCode();
            bill.ID = billService.Create(bill);
            return AjaxResult.Success(bill.ID);
        }
        #endregion

        #region bill template
        public AjaxResult SaveTemplate(BillTemplateDTO template)
        {
            if (template == null)
            {
                return AjaxResult.Error("template is null");
            }
            if (template.ID == 0)
            {
                template.UserId = ApplicationUser.Current.UserId;
                template.ID = billTemplateService.Create(template);
            }
            else
            {
                billTemplateService.Modify(template);
            }

            return AjaxResult.Success(template.ID);
        }
        [HttpGet]
        [HttpPost]
        public AjaxResult RemoveTemplate(int id)
        {
            billTemplateService.Remove(id);

            return AjaxResult.Success();
        }
        #endregion

        #region Remove
        [HttpPost]
        [HttpGet]
        public AjaxResult Remove(int id)
        {
            billService.Remove(id);
            return AjaxResult.Success();
        }
        #endregion

        #region AdjustBalance
        [HttpPost]
        [HttpGet]
        public AjaxResult AdjustBalance(AccountBalanceAdjustModel adjustParam)
        {
            var accountDTO = accountService.GetById(adjustParam.Id);
            var adjustPrice = adjustParam.Balance - accountDTO.Balance;
            var categoryInfo = categoryService.GetByName(ApplicationUser.Current.UserId,
                adjustPrice >= 0 ? CategoryType.Income.GetHashCode() : CategoryType.Expend.GetHashCode(),
                "漏记款");
            var bill = new BillDTO
            {
                IsBalanceAdjust = true,
                Price = adjustPrice,
                UserId = ApplicationUser.Current.UserId,
                AccountId = adjustParam.Id,
                ConsumeTime = DateTime.Now,
                DetailType =
                    adjustPrice >= 0 ? BillDetailType.Income.GetHashCode() : BillDetailType.Expend.GetHashCode(),
                BookId = 0,
                Remark = "陈年烂账引发的余额调整",
                SecondCategoryId = categoryInfo == null ? 0 : categoryInfo.Id,
                SecondCategory = categoryInfo == null ? string.Empty : categoryInfo.Name,
                FirstCategoryId = categoryInfo == null ? 0 : categoryInfo.ParentId,
            };
            billService.Create(bill);
            return AjaxResult.Success();
        }
        #endregion
    }
}
