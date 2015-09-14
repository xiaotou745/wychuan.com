using System.Web.Http;
using System.Web.Mvc;

namespace wychuan2.com.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapHttpRoute(
                name: "admin_defaultapi",
                routeTemplate: "admin/api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            context.MapRoute(
                "licai_default",
                "licai/{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                namespaces: new[] {"wychuan2.com.Areas.admin.Controllers.LiCai"}
                );

            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "wychuan2.com.Areas.admin.Controllers" }
            );
        }
    }
}