using System.Collections.Generic;

namespace wychuan2.com.Areas.admin.Models
{
    /// <summary>
    /// ������
    /// </summary>
    public class TableRow : List<TableRowItem>
    {
        #region NewRowItem

        public TableRowItem NewRowItem()
        {
            return new TableRowItem();
        }

        public TableRowItem NewRowItem(string name, string data)
        {
            return new TableRowItem {Name = name, Data = data};
        }

        public TableRowItem NewRowItem(string name, string data, string className)
        {
            return new TableRowItem {Name = name, Data = data, Class = className};
        }

        #endregion

        public TableRowItem AddItem(string name, string data, string className)
        {
            var tableRowItem = new TableRowItem {Name = name, Data = data, Class = className};
            Add(tableRowItem);
            return tableRowItem;
        }

        public TableRowItem AddItem(string name,string data,string className, string cloSpan, string rowSpan)
        {
            var tableRowItem = new TableRowItem()
                                   {Name = name, Data = data, Class = className, Colspan = cloSpan, Rowspan = rowSpan};
            Add(tableRowItem);
            return tableRowItem;
        }

        public TableRowItem AddItem(string name, string data)
        {
            var tableRowItem = new TableRowItem {Name = name, Data = data};
            Add(tableRowItem);
            return tableRowItem;
        }

        public void AddItem(TableRowItem tableRowItem)
        {
            Add(tableRowItem);
        }
    }
}