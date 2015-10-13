using System.Collections.Generic;
using System.Text;
using AC.Security;

namespace wychuan2.com.Areas.admin.Models.HtmlTable
{
    /// <summary>
    /// html table tr ->td������
    /// <td name="${Name}" id="${Id}" class="${Class}" rowspan="${Rowspan}" 
    ///     colspan="${Colspan}" ${Properties}></td>
    /// </summary>
    public abstract class HtmlTableTdItem
    {
        /// <summary>
        /// ����,��Ӧtd��ǩ��name����
        /// <td name="${Name}">...</td>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// td��id����ֵ
        /// <td id="${Id}">...</td>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// td�������class����
        /// <td class="${Class}">...</td>
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// td ��ǩ��rowspan����ֵ
        /// <td rowspan="${Rowspan}">...</td>
        /// </summary>
        public string Rowspan { get; set; }

        /// <summary>
        /// td ��ǩ��colspan����ֵ
        /// <td colspan="${Colspan}">...</td>
        /// </summary>
        public string Colspan { get; set; }

        /// <summary>
        /// td��ǩ����������
        /// <td ${Properties}>...</td>
        /// </summary>
        public string Properties { get; set; }

        /// <summary>
        /// �Ƿ���ʾ�����Ϊtrue������ʾ����
        /// </summary>
        public string Display { get; set; }

        public abstract string TextHtml();

        public override string ToString()
        {
            var tdProperties = new StringBuilder();
            if (!string.IsNullOrEmpty(Display) && "false".Equals(Display.ToLower()))
            {
                Class += " hide";
            }
            if (!string.IsNullOrEmpty(Name))
            {
                tdProperties.AppendFormat(" name=\"{0}\"", Name);
            }
            if (!string.IsNullOrEmpty(Id))
            {
                tdProperties.AppendFormat(" id=\"{0}\"", Id);
            }
            if (!string.IsNullOrEmpty(Class))
            {
                tdProperties.AppendFormat(" class=\"{0}\"", Class);
            }
            if (!string.IsNullOrEmpty(Rowspan))
            {
                tdProperties.AppendFormat(" rowspan=\"{0}\"", Rowspan);
            }
            if (!string.IsNullOrEmpty(Colspan))
            {
                tdProperties.AppendFormat(" colspan=\"{0}\"", Colspan);
            }
            if (!string.IsNullOrEmpty(Properties))
            {
                tdProperties.AppendFormat(" {0}", Properties);
            }
            return "<td" + tdProperties + ">" + TextHtml() + "</td>";
        }
    }

    /// <summary>
    /// ��ͨtext td��ǩ
    /// </summary>
    public class HtmlTableTdTextItem : HtmlTableTdItem
    {
        /// <summary>
        /// text�ı�
        /// <td>${Text}</td>
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// �Ƿ�ʹ��<code></code>��ǩ��ʾ����
        /// if true then
        ///     <td><code>${Text}</code></td>
        /// else then
        ///     <td>${Text}</td>
        /// </summary>
        public string UseCode { get; set; }

        /// <summary>
        /// Դ�����Ƿ�ͨ��DES����,���Ϊ<code>true</code>,����Ҫ���ܺ���ʾ
        /// if true then
        ///     <td>DES.Decrypt3DES(${Text})</td>
        /// else then
        ///     <td>${Text}</td>
        /// </summary>
        public string UseDES { get; set; }

        public override string TextHtml()
        {
            string text = Text;
            if (!string.IsNullOrEmpty(UseDES) && "true".Equals(UseDES.ToLower()))
            {
                text = DES.Decrypt3DES(text);
            }
            if (!string.IsNullOrEmpty(UseCode) && "true".Equals(UseCode.ToLower()))
            {
                text = "<code>" + text + "</code>";
            }
            return text;
        }
    }

    /// <summary>
    /// ���� td ��ǩ
    /// </summary>
    public class HtmlTableTdLinkItem : HtmlTableTdItem
    {
        private IList<LinkItem> linkItems = new List<LinkItem>();

        public IList<LinkItem> LinkItems
        {
            get
            {
                return linkItems;
            }
            set
            {
                linkItems = value;
            }
        }

        public override string TextHtml()
        {
            var sb = new StringBuilder();

            if (LinkItems != null && LinkItems.Count > 0)
            {
                foreach (var item in LinkItems)
                {
                    sb.Append(item);
                    sb.Append("&nbsp;&nbsp;");
                }
            }

            return sb.ToString();
        }
    }
}