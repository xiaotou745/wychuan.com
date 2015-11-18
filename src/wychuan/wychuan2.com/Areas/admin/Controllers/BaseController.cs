using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AC.Service.DTO.User;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //是否登录
            if (ApplicationUser.Current.UserId == 0)
            {
                Response.Redirect("/admin/account/logon");
            }

            //是否有权限，如果没有权限，跳转到首页
            string controller = AdminUtils.CurrentController();
            string action = AdminUtils.CurrentAction();
            IList<MenuDTO> lstMenus = AdminUtils.GetMenus();
            MenuDTO currentMenu =
                lstMenus.FirstOrDefault(
                    m =>
                        m.Controller.Equals(controller, System.StringComparison.InvariantCultureIgnoreCase) &&
                        m.Action.Equals(action, System.StringComparison.InvariantCultureIgnoreCase));
            if (currentMenu != null)
            {
                IList<int> userMenuIds = AdminUtils.GetUserPrivilege();
                if (!userMenuIds.Contains(currentMenu.Id))
                {
                    Response.Redirect("/admin");
                }
            }

            ViewBag.ShowTopNav = true;
            base.OnActionExecuting(filterContext);
        }
    }
}