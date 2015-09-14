using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wychuan2.com.Areas.admin.Controllers.LiCai
{
    public class BillController : Controller
    {
        // GET: licai/bill
        public ActionResult Index()
        {
            return View();
        }

        // GET: admin/Bill/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: admin/Bill/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Bill/Create
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

        // GET: admin/Bill/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: admin/Bill/Edit/5
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

        // GET: admin/Bill/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: admin/Bill/Delete/5
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
