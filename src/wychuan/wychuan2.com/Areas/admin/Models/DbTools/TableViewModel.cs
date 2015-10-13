using System.Collections.Generic;
using wychuan2.com.Areas.admin.Models.HtmlTable;

namespace wychuan2.com.Areas.admin.Models.DbTools
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
        public HtmlTableInfo HtmlTableResult { get; set; }

        /// <summary>
        /// 是否含有编辑按钮 
        /// </summary>
        public string TableHasEditOper { get; set; }
        /// <summary>
        /// 是否含有生成代码按钮
        /// </summary>
        public string TableHasGenerateCodeOper { get; set; }

        private CodeResult codeResult = CodeResult.Empty;

        /// <summary>
        /// 代码生成结果
        /// </summary>
        public CodeResult CodeResult
        {
            get { return codeResult; }
            set { codeResult = value; }
        }

        public string DbNameSource { get; set; }
        public string DbTableSource { get; set; }
    }
}