using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AC.Service.DTO.Tools;
using AC.Service.Impl.Tools;
using AC.Service.Tools;
using AC.Web;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers.api
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService companyService = new CompanyService();
        private readonly ICompanyTimelineService timelineService = new CompanyTimelineService();

        [HttpPost]
        [HttpGet]
        public AjaxResult Save(CompanyDTO company)
        {
            if (company == null)
            {
                return AjaxResult.Error("请输入任务内容");
            }

            if (company.Id == 0)
            {
                company.UserId = ApplicationUser.Current.UserId;

                company.Id = companyService.Create(company);
            }
            //else
            //{
            //    mytaskService.Modify(task);
            //}
            return AjaxResult.Success();
        }
    }
}
