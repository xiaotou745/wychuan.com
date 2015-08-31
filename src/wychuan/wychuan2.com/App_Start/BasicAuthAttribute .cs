using System;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

using System.Web.Mvc;
using System.Web.Mvc.Filters;


namespace wychuan2.com.App_Start
{
    public class BasicAuthAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            IPrincipal user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}
