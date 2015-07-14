using Common.Logging;

namespace AC.Service
{
    public class AppLogger
    {
        public static ILog Logger
        {
            get { return LogManager.GetCurrentClassLogger(); }
        } 

        public static ILog LoggerOfError
        {
            get { return LogManager.GetLogger("ErrorLog"); }
        }

        public static ILog LoggerOfWeiXin
        {
            get { return LogManager.GetLogger("WeixinLog"); }
        } 
    }
}
