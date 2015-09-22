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

        public ActionResult PinBoard()
        {
            return View();
        }

        // GET: admin/Tools/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: admin/Tools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Tools/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: admin/Tools/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: admin/Tools/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: admin/Tools/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: admin/Tools/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
