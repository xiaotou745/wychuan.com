using System.Collections.Generic;
using System.Text;

namespace wychuan2.com.Areas.admin.Models.HtmlTable
{
    /// <summary>
    /// ÐÐÊý¾Ý
    /// </summary>
    public class HtmlTableTrItem : List<HtmlTableTdItem>
    {
        #region NewTd

        public HtmlTableTdItem NewTd()
        {
            var td = new HtmlTableTdTextItem();
            Add(td);
            return td;
        }

        public HtmlTableTdItem NewTd(string name, string text)
        {
            return NewTd(name, text, null);
        }

        public HtmlTableTdItem NewTd(string name, string text, string className)
        {
            var td = new HtmlTableTdTextItem { Name = name, Text = text, Class = className };
            Add(td);
            return td;
        }

        #endregion

        #region NewLinkTd

        public HtmlTableTdItem NewLinkTd(string name, string href, string text)
        {
            return NewLinkTd(name, href, null, text);
        }

        public HtmlTableTdItem NewLinkTd(string name, string href, string classInfo, string text)
        {
            var linkTd = new HtmlTableTdLinkItem {Name = name, Class = classInfo};
            linkTd.LinkItems.Add(new LinkItem
            {
                Text = text,
                Href = href,
            });
            Add(linkTd);
            return linkTd;
        }
        #endregion

        #region AddTd
        public HtmlTableTdItem AddTd(string name, string data, string className)
        {
            var tableRowItem = new HtmlTableTdTextItem { Name = name, Text = data, Class = className };
            Add(tableRowItem);
            return tableRowItem;
        }

        public HtmlTableTdItem AddTd(string name,string data,string className, string cloSpan, string rowSpan)
        {
            var tableRowItem = new HtmlTableTdTextItem { Name = name, Text = data, Class = className, Colspan = cloSpan, Rowspan = rowSpan };
            Add(tableRowItem);
            return tableRowItem;
        }

        public HtmlTableTdItem AddTd(string name, string data)
        {
            var tableRowItem = new HtmlTableTdTextItem { Name = name, Text = data };
            Add(tableRowItem);
            return tableRowItem;
        }

        public void AddTd(HtmlTableTdItem htmlTableTdItem)
        {
            Add(htmlTableTdItem);
        }
        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder("<tr>");
            foreach (var item in this)
            {
                sb.Append(item);
            }
            sb.Append("</tr>");
            return sb.ToString();
        }
    }
}