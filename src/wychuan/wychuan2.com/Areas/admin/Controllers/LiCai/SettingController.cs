using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wychuan2.com.Areas.admin.Controllers.LiCai
{
    public class SettingController : BaseController
    {
        // GET: admin/Setting
        public ActionResult Index()
        {
            return View();
        }

        // GET: admin/Setting/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: admin/Setting/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Setting/Create
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

        // GET: admin/Setting/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: admin/Setting/Edit/5
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

        // GET: admin/Setting/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: admin/Setting/Delete/5
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
