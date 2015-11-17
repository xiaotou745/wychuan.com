using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC;
using AC.Service.DTO.LiCai;
using AC.Service.DTO.Tools;
using AC.Service.Impl.LiCai;
using AC.Service.Impl.Tools;
using AC.Service.LiCai;
using AC.Service.Tools;
using AC.Web;
using wychuan2.com.Areas.admin.Models;
using wychuan2.com.Areas.admin.Models.LiCai;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers
{
    public class ToolsController : BaseController
    {
        private readonly IMyTaskService myTaskService = new MyTaskService();
        private readonly ICompanyService companyService = new CompanyService();
        private readonly IMyNotesService myNotesService = new MyNotesService();

        #region Jobs
        // GET: admin/Tools
        public ActionResult Jobs()
        {
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
            var queryInfo = new MyNotesQueryInfo
            {
                UserId = ApplicationUser.Current.UserId,
                CreateTimeRange =
                    new AC.StartOverTimePair {StartTime = DateTime.Now.AddDays(-7), OverTime = DateTime.Now}
            };

            IList<MyNotesDTO> notes = myNotesService.Query(queryInfo);
            return View(notes);
        }

        public AjaxResult SaveNotes(MyNotesDTO note)
        {
            if (note == null)
            {
                return AjaxResult.Error("note is null");
            }
            if (note.Id == 0)
            {
                note.UserId = ApplicationUser.Current.UserId;
                note.Id = myNotesService.Create(note);
            }
            return AjaxResult.Success(note.Id);
        }

        public AjaxResult RemoveNotes(int id)
        {
            if (id <= 0)
            {
                return AjaxResult.Error("id <= 0");
            }
            myNotesService.Remove(id);
            return AjaxResult.Success();
        }

        public ActionResult RefreshNotes(StartOverTimePair timeRange)
        {
            var queryInfo = new MyNotesQueryInfo {UserId = ApplicationUser.Current.UserId};
            if (timeRange != null)
            {
                queryInfo.CreateTimeRange = timeRange;
            }
            else
            {
                queryInfo.CreateTimeRange = new StartOverTimePair
                {
                    StartTime = DateTime.Now.AddDays(-7),
                    OverTime = DateTime.Now
                };
            }
            
            IList<MyNotesDTO> notes = myNotesService.Query(queryInfo);
            return View("_PinBoardList", notes);
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
        private readonly IAccountService accountService = new AccountService();
        private readonly IItemsService itemService = new ItemsService();
        private readonly ICategoryService categoryService = new CategoryService();
        public ActionResult Algorithm()
        {
            return View();
        }
        #endregion

        #region 表单构建器

        public ActionResult FormBuilder()
        {
            return View();
        }
        #endregion

        #region Wyswig

        public ActionResult Wyswig()
        {
            return View();
        }
        #endregion

        #region Image Upload

        public ActionResult Upload()
        {
            return View();
        }
        #endregion
    }
}
