using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AC.Service.DTO.Tools;

namespace wychuan2.com.Areas.admin.Models
{
    public class MyTaskModel
    {
        /// <summary>
        /// 我的任务列表
        /// </summary>
        public IList<MyTaskDTO> MyTasks { get; set; }
    }
}