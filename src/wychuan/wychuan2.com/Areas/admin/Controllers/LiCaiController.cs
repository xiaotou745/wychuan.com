using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Service.DTO.LiCai;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers
{
    
    public class LiCaiController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Account(int type=0)
        {
            var model = new LiCaiAccount();

            List<AccountType> accountTypes = AccountType.Create(Server.MapPath("~/data/lc_accounttype.config"),true).GetAccountTypes();

            model.AccountTypes = accountTypes;
            model.CurrentType = accountTypes.FirstOrDefault(a => a.Id == type);

            return View(model);;
        }

        public ActionResult AccountAdd()
        {
            var model = new LiCaiAccount();

            List<AccountType> accountTypes = AccountType.Create(Server.MapPath("~/data/lc_accounttype.config"), true).GetAccountTypes();

            model.AccountTypes = accountTypes;

            return View(model);
        }

        // GET: admin/LiCaiAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/LiCaiAccount/Create
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

        // GET: admin/LiCaiAccount/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: admin/LiCaiAccount/Edit/5
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

        // GET: admin/LiCaiAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: admin/LiCaiAccount/Delete/5
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
