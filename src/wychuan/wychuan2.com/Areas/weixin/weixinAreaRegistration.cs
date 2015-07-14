using System.Web.Http;
using System.Web.Mvc;

namespace wychuan2.com.Areas.weixin
{
    public class weixinAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "weixin";
            }
        }


        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapHttpRoute(
                name: "weixin_defaultapi",
                routeTemplate: "weixin/api/{controller}/{action}/{id}",
                defaults: new { controller="Processor",action="Index", id = RouteParameter.Optional }
            );

            context.MapRoute(
                "weixin_default",
                "weixin/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "wychuan2.com.Areas.weixin.Controllers" }
            );
        }
    }
}