using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wychuan2.com.Areas.admin.Controllers.LiCai
{
    public class YuSuanController : BaseController
    {
        // GET: admin/YuSuan
        public ActionResult Index()
        {
            return View();
        }

        // GET: admin/YuSuan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: admin/YuSuan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/YuSuan/Create
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

        // GET: admin/YuSuan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: admin/YuSuan/Edit/5
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

        // GET: admin/YuSuan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: admin/YuSuan/Delete/5
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
