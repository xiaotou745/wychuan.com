using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using AC.Service;
using AC.Web.Common.Filter;

namespace wychuan2.com
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new WeiXinHandleErrorAttribute());
            //config.Filters.Add(new WeiXinMsgLog());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
