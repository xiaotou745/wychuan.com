using System.Collections.Generic;

namespace wychuan2.com.Areas.admin.Models
{
    /// <summary>
    /// ����Ϣ
    /// </summary>
    public class CommonTableInfo
    {
        #region ���캯��

        /// <summary>
        /// ��������
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
        /// �Ƿ�ʹ�ð���������ʽ
        /// </summary>
        public bool UseStripped { get; set; }

        /// <summary>
        /// ����,��������
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