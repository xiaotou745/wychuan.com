using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Common.Logging;

namespace AC.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    public class WeiXinMsgLog : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            ILog logger = LogManager.GetLogger("WeixinLog");
            Task<string> readAsStringAsync = actionContext.Request.Content.ReadAsStringAsync();
            readAsStringAsync.Wait();
            logger.Info(readAsStringAsync.Result);

            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }

    public class WeiXinHandleErrorAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            WriteException(actionExecutedContext.Exception);
        }

        private void WriteException(Exception error)
        {
            ILog logger = LogManager.GetLogger("ErrorLog");
            try
            {
                string logstr = "\r\n-----------------start----------------------\r\n";
                logstr = logstr + DateTime.Now + "\r\n";
                //异常发生地址
                logstr = logstr + "异常发生地址:" + HttpContext.Current.Request.Url.AbsoluteUri + "\r\n";
                logstr = logstr + "请求类型:" + HttpContext.Current.Request.RequestType + "\r\n";

                logstr = logstr + "异常:" + error + "\r\n";

                logstr += "--------------------end---------------------\r\n";
                //发送邮件
                //if (ConfigSettings.Instance.IsSendMail == "true")
                //{
                //    string emailToAddress = ConfigSettings.Instance.EmailToAdress;
                //    EmailHelper.SendEmailTo(logstr, emailToAddress);
                //}
                //写日志
                logger.Error(logstr, error);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}