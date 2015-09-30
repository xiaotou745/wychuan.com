using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wychuan2.com.Areas.admin.Models
{
    public class TableViewModel
    {
        /// <summary>
        /// 当前显示的内容类型:1数据库列表，2表列表 3表详细信息
        /// </summary>
        public int CurrentViewType { get; set; }

        /// <summary>
        /// 服务器列表
        /// </summary>
        public IList<string> DbServers { get; set; }

        /// <summary>
        /// 当前服务器
        /// </summary>
        public string CurrentDbServer { get; set; }

        /// <summary>
        /// 当前数据库名称
        /// </summary>
        public string CurrentDbName { get; set; }

        /// <summary>
        /// 当前选择的表名称
        /// </summary>
        public string CurrentTableName { get; set; }

        /// <summary>
        /// HtmlTable信息
        /// </summary>
        public CommonTableInfo HtmlTableResult { get; set; }

        public string DbNameSource { get; set; }
        public string DbTableSource { get; set; }
    }
}