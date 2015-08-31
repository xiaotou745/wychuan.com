using System.Web.Mvc;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (ApplicationUser.Current.UserId == 0)
            {
                Response.Redirect("/admin/account/logon");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}