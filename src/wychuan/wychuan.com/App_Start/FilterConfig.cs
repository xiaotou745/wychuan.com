using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AC.Service;
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

}