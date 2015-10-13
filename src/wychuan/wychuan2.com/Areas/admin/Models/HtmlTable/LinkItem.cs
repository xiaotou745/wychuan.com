using System.Text;

namespace wychuan2.com.Areas.admin.Models.HtmlTable
{
    /// <summary>
    /// 链接项 <a>标签
    /// <a ${Properties} href="${Href}">${Text}</a>
    /// </summary>
    public class LinkItem
    {
        /// <summary>
        /// a text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// href
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// a 的属性
        /// <a properties></a>
        /// </summary>
        public string Properties { get; set; }

        public override string ToString()
        {
            //<a ${Properties} href="${Href}">${Text}</a>
            var sb = new StringBuilder();
            sb.AppendFormat("<a href=\"{0}\"", Href);
            if (!string.IsNullOrEmpty(Properties))
            {
                sb.AppendFormat(" {0}", Properties);
            }
            sb.AppendFormat(">{0}</a>", Text);
            return sb.ToString();
        }
    }
}