using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Service.DTO.LiCai;
using AC.Service.Impl.LiCai;
using AC.Service.LiCai;
using wychuan2.com.Areas.admin.Models.LiCai;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers.LiCai
{
    public class BillController : BaseController
    {
        private readonly IAccountService accountService = new AccountService();
        //private readonly IBillBookService bookService = new BillBookService();
        //private readonly IMemberService memberService = new MemberService();
        //private readonly IBusinessService businessService = new BusinessService();
        //private readonly IProjectService projectService = new ProjectService();
        private readonly IItemsService itemService = new ItemsService();
        private readonly ICategoryService categoryService = new CategoryService();

        // GET: licai/bill
        public ActionResult Index()
        {
            BillModel model = new BillModel();

            //model.Books = bookService.GetByUserId(ApplicationUser.Current.UserId);
            //model.Members = memberService.GetByUserId(ApplicationUser.Current.UserId);
            model.Accounts = accountService.Query(new AccountQueryInfo {UserId = ApplicationUser.Current.UserId});
            //model.Business = businessService.GetByUserId(ApplicationUser.Current.UserId);
            //model.Projects = projectService.GetByUserId(ApplicationUser.Current.UserId);
            model.Items = itemService.GetByUserId(ApplicationUser.Current.UserId);
            model.Categories = categoryService.GetByUserId(ApplicationUser.Current.UserId);

            return View(model);
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
