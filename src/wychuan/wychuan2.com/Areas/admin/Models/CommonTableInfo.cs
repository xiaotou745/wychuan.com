using System.Collections.Generic;

namespace wychuan2.com.Areas.admin.Models
{
    /// <summary>
    /// 表信息
    /// </summary>
    public class CommonTableInfo
    {
        #region 构造函数

        /// <summary>
        /// 表数据行
        /// </summary>
        public IList<TableRow> DataRows;

        public CommonTableInfo()
        {
            UseSection = false;
            DataRows = new List<TableRow>();
            ColumnNames = new ColumnNames();
        }

        #endregion

        #region Section

        public bool UseSection { get; set; }

        public string SectionId { get; set; }
        public string SectionName { get; set; }

        #endregion

        /// <summary>
        /// 是否使用斑马条纹样式
        /// </summary>
        public bool UseStripped { get; set; }

        /// <summary>
        /// 列名,列描述名
        /// </summary>
        public ColumnNames ColumnNames { get; set; }

        public bool HasColumns
        {
            get { return ColumnNames.Count > 0; }
        }

        public void AddRow(TableRow tableRow)
        {
            DataRows.Add(tableRow);
        }
    }
}