using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wychuan2.com.Areas.admin.Controllers.LiCai
{
    public class DetailsController : Controller
    {
        // GET: admin/Details
        public ActionResult Index()
        {
            return View();
        }

        // GET: admin/Details/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: admin/Details/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Details/Create
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

        // GET: admin/Details/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: admin/Details/Edit/5
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

        // GET: admin/Details/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: admin/Details/Delete/5
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
