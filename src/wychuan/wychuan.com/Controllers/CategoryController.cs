using System.Web.Mvc;
using AC.Web.Common.Config;
using Common.Logging;

namespace AC.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService = new CategoryConfigHelper();
        //
        // GET: /Category/

        public ActionResult Index()
        {
            var lstCategories = categoryService.GetAll();

            return View(lstCategories);
        }
    }
}