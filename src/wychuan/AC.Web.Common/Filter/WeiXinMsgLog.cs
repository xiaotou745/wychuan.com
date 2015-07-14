using System.Threading.Tasks;
using System.Web.Http.Filters;
using AC.Service;

namespace AC.Web.Common.Filter
{
    /// <summary>
    /// 微信消息日志filter
    /// </summary>
    public class WeiXinMsgLog :ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            Task<string> readAsStringAsync = actionContext.Request.Content.ReadAsStringAsync();
            readAsStringAsync.Wait();
            AppLogger.LoggerOfWeiXin.Info(readAsStringAsync.Result);

            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }

}
