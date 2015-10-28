using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wychuan2.com.Controllers
{
    public class LeLeController : Controller
    {
        // GET: LeLe
        public ActionResult Index()
        {
            var tasks = LeLeTasks.GetTasks();
            ViewBag.Tasks = tasks;
            var taskInfos = LeLeTasks.GetTaskInfo();
            ViewBag.TaskInfos = taskInfos;
            return View();
        }
    }

    public class TaskInfo
    {
        /// <summary>
        /// 类型 1:班工作 2：日工作 3：周工作 4：月工作 5：季任务
        /// </summary>
        public int Type { get; set; }

        public string TaskName { get; set; }

        public static TaskInfo New(int type, string taskName)
        {
            return new TaskInfo {Type = type, TaskName = taskName};
        }
    }
    public class LeLeTasks
    {
        public static IList<string> GetTasks()
        {
            IList<string> result = new List<string>();
            result.Add("接班后试声、光信号一次");
            result.Add("活动油位计一次并校验就地与CRT油位数值");
            result.Add("核对就地与表盘水位计一次");
            result.Add("机组测振动一次（接班后两小时）");
            result.Add("未投抽气逆止门前疏水两次（接班后、接班前2小时）");
            result.Add("泵坑、阀门坑排水一次");
            result.Add("检查运行及备用油箱、泵组油位");

            result.Add("每日夜班试机电联系型号");
            result.Add("每日白班活动自动主汽门一次");
            result.Add("每日白班胶球清洗");

            var currentDay = DateTime.Now;
            switch (currentDay.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    break;
                case DayOfWeek.Monday:
                    result.Add("试验倒换循环水泵、消防泵、稳压泵");
                    break;
                case DayOfWeek.Tuesday:
                    result.Add("试验倒换给水泵、稀油泵");
                    break;
                case DayOfWeek.Wednesday:
                    result.Add("试验倒换凝结水泵、低加疏水泵");
                    break;
                case DayOfWeek.Thursday:
                    result.Add("试验倒换射水泵及抽气器");
                    break;
                case DayOfWeek.Friday:
                    result.Add("试验启动油泵、交流润滑油泵、直流润滑油泵、顶轴油泵");
                    break;
                case DayOfWeek.Saturday:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (currentDay.Day == 6 || currentDay.Day == 21)
            {
                result.Add("活动三抽快关阀");
            }
            if (currentDay.Day == 8)
            {
                result.Add("检修清理冷油器疏水网，清理水塔出口滤水网");
            }
            if (currentDay.Day == 9 || currentDay.Day == 24)
            {
                result.Add("试验高压电磁阀");
            }
            if (currentDay.Day == 10 || currentDay.Day == 25)
            {
                result.Add("变动负荷");
            }
            if (currentDay.Day == 12 || currentDay.Day == 27)
            {
                result.Add("疏水箱、低位水箱放水冲洗水位计");
            }
            if (currentDay.Day == 13 || currentDay.Day == 28)
            {
                result.Add("试验抽气电磁阀，抽气逆止门逐个试验");
            }
            if (currentDay.Day == 15)
            {
                result.Add("真空严密性试验");
            }
            if (currentDay.Day == 18)
            {
                result.Add("由检修往主、辅油箱补油至正常，泵组定期加、换油");
            }

            if (currentDay.Month%3 == 1 && currentDay.Day == 15)
            {
                result.Add("除氧器安全门手动排气一次");
            }
            if (currentDay.Month%3 == 2 && currentDay.Day == 15)
            {
                result.Add("汽机三抽安全门手动排气一次");
            }
            return result;
        }

        public static IList<TaskInfo> GetTaskInfo()
        {
            IList<TaskInfo> result = new List<TaskInfo>();
            result.Add(TaskInfo.New(1,"接班后试声、光信号一次"));
            result.Add(TaskInfo.New(1,"活动油位计一次并校验就地与CRT油位数值"));
            result.Add(TaskInfo.New(1,"核对就地与表盘水位计一次"));
            result.Add(TaskInfo.New(1,"机组测振动一次（接班后两小时）"));
            result.Add(TaskInfo.New(1,"未投抽气逆止门前疏水两次（接班后、接班前2小时）"));
            result.Add(TaskInfo.New(1,"泵坑、阀门坑排水一次"));
            result.Add(TaskInfo.New(1,"检查运行及备用油箱、泵组油位"));

            result.Add(TaskInfo.New(2,"每日夜班试机电联系型号"));
            result.Add(TaskInfo.New(2,"每日白班活动自动主汽门一次"));
            result.Add(TaskInfo.New(2,"每日白班胶球清洗"));

            var currentDay = DateTime.Now;
            switch (currentDay.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    break;
                case DayOfWeek.Monday:
                    result.Add(TaskInfo.New(3,"试验倒换循环水泵、消防泵、稳压泵"));
                    break;
                case DayOfWeek.Tuesday:
                    result.Add(TaskInfo.New(3,"试验倒换给水泵、稀油泵"));
                    break;
                case DayOfWeek.Wednesday:
                    result.Add(TaskInfo.New(3,"试验倒换凝结水泵、低加疏水泵"));
                    break;
                case DayOfWeek.Thursday:
                    result.Add(TaskInfo.New(3,"试验倒换射水泵及抽气器"));
                    break;
                case DayOfWeek.Friday:
                    result.Add(TaskInfo.New(3,"试验启动油泵、交流润滑油泵、直流润滑油泵、顶轴油泵"));
                    break;
                case DayOfWeek.Saturday:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (currentDay.Day == 6 || currentDay.Day == 21)
            {
                result.Add(TaskInfo.New(4,"活动三抽快关阀"));
            }
            if (currentDay.Day == 8)
            {
                result.Add(TaskInfo.New(4,"检修清理冷油器疏水网，清理水塔出口滤水网"));
            }
            if (currentDay.Day == 9 || currentDay.Day == 24)
            {
                result.Add(TaskInfo.New(4,"试验高压电磁阀"));
            }
            if (currentDay.Day == 10 || currentDay.Day == 25)
            {
                result.Add(TaskInfo.New(4,"变动负荷"));
            }
            if (currentDay.Day == 12 || currentDay.Day == 27)
            {
                result.Add(TaskInfo.New(4,"疏水箱、低位水箱放水冲洗水位计"));
            }
            if (currentDay.Day == 13 || currentDay.Day == 28)
            {
                result.Add(TaskInfo.New(4,"试验抽气电磁阀，抽气逆止门逐个试验"));
            }
            if (currentDay.Day == 15)
            {
                result.Add(TaskInfo.New(4,"真空严密性试验"));
            }
            if (currentDay.Day == 18)
            {
                result.Add(TaskInfo.New(4,"由检修往主、辅油箱补油至正常，泵组定期加、换油"));
            }

            if (currentDay.Month % 3 == 1 && currentDay.Day == 15)
            {
                result.Add(TaskInfo.New(5,"除氧器安全门手动排气一次"));
            }
            if (currentDay.Month % 3 == 2 && currentDay.Day == 15)
            {
                result.Add(TaskInfo.New(5,"汽机三抽安全门手动排气一次"));
            }
            return result;
        }
    }
}