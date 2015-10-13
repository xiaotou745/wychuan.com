using System.Collections.Generic;
using System.Text;

namespace wychuan2.com.Areas.admin.Models.HtmlTable
{
    /// <summary>
    /// ����Ϣ
    /// </summary>
    public class HtmlTableInfo
    {
        #region ���캯��

        /// <summary>
        /// ��������
        /// </summary>
        public IList<HtmlTableTrItem> DataRows;

        public HtmlTableInfo()
        {
            UseSection = false;
            DataRows = new List<HtmlTableTrItem>();
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

        public void AddRow(HtmlTableTrItem htmlTableTrItem)
        {
            DataRows.Add(htmlTableTrItem);
        }

        public HtmlTableTrItem NewRow()
        {
            var trItem = new HtmlTableTrItem();
            AddRow(trItem);
            return trItem;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<table class=\"table table-bordered table-condensed {0}\">",
                UseStripped ? "table-striped" : string.Empty);
            sb.Append("<thead><tr>");
            foreach (var colName in ColumnNames)
            {
                sb.AppendFormat("<th>{0}</th>", colName);
            }
            sb.Append("</tr></thead><tbody>");
            foreach (var tableRow in DataRows)
            {
                sb.Append(tableRow);
            }
            sb.Append("</tbody></table>");
            return sb.ToString();
        }
    }
}