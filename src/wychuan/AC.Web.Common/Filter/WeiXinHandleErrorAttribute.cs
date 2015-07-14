using System;
using System.Web;
using AC.Service;
using Common.Logging;

namespace AC.Web.Common.Filter
{
    public class WeiXinHandleErrorAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            WriteException(actionExecutedContext.Exception);
        }

        private void WriteException(Exception error)
        {
            ILog logger = AppLogger.LoggerOfError;
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
