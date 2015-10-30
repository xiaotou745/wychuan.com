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
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers.api
{
    public class BillController : ApiController
    {
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
    }
}
