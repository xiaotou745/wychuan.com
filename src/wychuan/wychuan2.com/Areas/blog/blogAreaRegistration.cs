using System.Web.Mvc;

namespace wychuan2.com.Areas.blog
{
    public class blogAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "blog";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //mydomain.com/blog/1
            context.MapRoute(
                "blog_default3",
                "blog/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "wychuan2.com.Areas.blog.Controllers" }
                );
            //mydomain.com/blog/category/java
            context.MapRoute(
                "blog_default2",
                "blog/{controller}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "wychuan2.com.Areas.blog.Controllers" }
                );
            //mydomain.com/blog/home/list/java
            context.MapRoute(
                "blog_default",
                "blog/{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                namespaces: new[] {"wychuan2.com.Areas.blog.Controllers"}
                );
        }
    }
}