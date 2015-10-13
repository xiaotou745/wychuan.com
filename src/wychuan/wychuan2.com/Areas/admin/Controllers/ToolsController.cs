using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Service.DTO.Tools;
using AC.Service.Impl.Tools;
using AC.Service.Tools;
using wychuan2.com.Areas.admin.Models;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers
{
    public class ToolsController : BaseController
    {
        private readonly IMyTaskService myTaskService = new MyTaskService();
        private readonly ICompanyService companyService = new CompanyService();
        
        #region Jobs
        // GET: admin/Tools
        public ActionResult Jobs()
        {
            ViewBag.ShowTopNav = false;
            var model = new MyTaskModel();

            var queryInfo = new MyTaskQueryInfo
            {
                UserId = ApplicationUser.Current.UserId,
                //StartDate = DateTime.Now.AddDays(-30),
                //EndDate = DateTime.Now
            };

            model.MyTasks = myTaskService.GetMyTasks(queryInfo);

            return View(model);
        }

        public ActionResult JobList()
        {
            var model = new MyTaskModel();

            var queryInfo = new MyTaskQueryInfo
            {
                UserId = ApplicationUser.Current.UserId,
                //StartDate = DateTime.Now.AddDays(-30),
                //EndDate = DateTime.Now
            };

            model.MyTasks = myTaskService.GetMyTasks(queryInfo);

            return View("_JobList", model);
        }

        #endregion

        #region Pin board
        public ActionResult PinBoard()
        {
            return View();
        }
        #endregion

        #region Companys

        public ActionResult Company()
        {
            CompanyModel model = new CompanyModel();

            model.Companys = companyService.GetByUserId(ApplicationUser.Current.UserId);

            return View(model);
        }

        public ActionResult CompanyList()
        {
            CompanyModel model = new CompanyModel();

            model.Companys = companyService.GetByUserId(ApplicationUser.Current.UserId);

            return View("_CompanyList", model);
        }

        public ActionResult CompanyDetail(int id)
        {
            return View();
        }
        #endregion

        #region Algorithm算法

        public ActionResult Algorithm()
        {
            return View();
        }
        #endregion
    }
}
