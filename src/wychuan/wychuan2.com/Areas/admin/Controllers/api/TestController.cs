using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace wychuan2.com.Areas.admin.Controllers.api
{
    public class TestController : ApiController
    {
        [HttpGet]
        public string hello()
        {
            return "hello";
        }
    }
}
