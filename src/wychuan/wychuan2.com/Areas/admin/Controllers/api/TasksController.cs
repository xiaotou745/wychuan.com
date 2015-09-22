using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AC.Service.DTO.Tools;
using AC.Service.Impl.Tools;
using AC.Service.Tools;
using AC.Web;
using wychuan2.com.Models;

namespace wychuan2.com.Areas.admin.Controllers.api
{
    public class TasksController : ApiController
    {
        private readonly IMyTaskService mytaskService = new MyTaskService();

        [HttpPost]
        public AjaxResult Save(MyTaskDTO task)
        {
            if (task == null)
            {
                return AjaxResult.Error("请输入任务内容");
            }
            if (string.IsNullOrEmpty(task.Content))
            {
                return AjaxResult.Error("任务内容不允许为空.");
            }

            if (task.Id == 0)
            {
                task.UserId = ApplicationUser.Current.UserId;

                task.Id = mytaskService.Create(task);
            }
            else
            {
                mytaskService.Modify(task);
            }
            return AjaxResult.Success();
        }

        public AjaxResult InProcess(int id)
        {
            mytaskService.ToInProcess(id);

            return AjaxResult.Success();
        }

        public AjaxResult Completed(int id)
        {
            mytaskService.ToCompleted(id);

            return AjaxResult.Success();
        }

        [HttpGet]
        [HttpPost]
        public AjaxResult ModifyStatus(int id, int status)
        {
            mytaskService.ModifyStatus(id, status);
            return AjaxResult.Success();
        }

        [HttpGet]
        [HttpPost]
        public AjaxResult Hide(int id)
        {
            mytaskService.Hide(id);
            return AjaxResult.Success();
        }
    }
}
