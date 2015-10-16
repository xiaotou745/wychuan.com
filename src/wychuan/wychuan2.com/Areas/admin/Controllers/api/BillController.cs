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
        private readonly IBillService billService = new BillService();

        public AjaxResult Save(BillDTO bill)
        {
            if (bill == null)
            {
                return AjaxResult.Error("bill is null");
            }
            if (bill.ID == 0)
            {
                bill.UserId = ApplicationUser.Current.UserId;
                bill.ID = billService.Create(bill);
            }

            return AjaxResult.Success(bill.ID);
        }
    }
}
