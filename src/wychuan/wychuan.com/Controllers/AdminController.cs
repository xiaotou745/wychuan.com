using System.Web.Mvc;
using AC.Web.Models;

namespace AC.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login(LoginModel loginModel)
        {
            return View("Index");
        }


        public ActionResult Index()
        {
            return View();
        }
    }
}