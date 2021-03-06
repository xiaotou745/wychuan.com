﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Service.DTO.Tools
{
    /// <summary>
    /// 任务状态
    /// </summary>
    public enum TaskStatus
    {
        ToDo=1,
        InProcess=2,
        Completed=3,
    }
    /// <summary>
    /// 任务优先级
    /// </summary>
    public enum TaskPriorityLevel
    {
        /// <summary>
        /// 普通
        /// </summary>
        [Description("普通")]
        Normal=0,
        /// <summary>
        /// 中级
        /// </summary>
        [Description("中级")]
        Middle = 1,
        /// <summary>
        /// 高级
        /// </summary>
        [Description("高级")]
        Senior = 2,
    }

    public class MyTaskDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户ID，如果为0，则为默认数据
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 1、未接受 2、进行中 3、已完成
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PubTime { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEnable { get; set; }

        public string Class { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompleteTime { get; set; }

        /// <summary>
        /// 优先级级别
        /// </summary>
        public int PriorityLevel { get; set; }
    }

    /// <summary>
    /// 我的任务查询条件
    /// </summary>
    public class MyTaskQueryInfo
    {
        public int UserId { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
    }
}