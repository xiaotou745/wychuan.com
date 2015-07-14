using System.Web.Http;
using AC.Web.Common.Filter;

namespace AC.Web.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.MapHttpAttributeRoutes();

            config.Filters.Add(new WeiXinHandleErrorAttribute());
            config.Filters.Add(new WeiXinMsgLog());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
