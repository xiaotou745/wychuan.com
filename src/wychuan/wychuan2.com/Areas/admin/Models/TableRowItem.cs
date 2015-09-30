namespace wychuan2.com.Areas.admin.Models
{
    /// <summary>
    /// td������
    /// </summary>
    public class TableRowItem
    {
        /// <summary>
        /// ����,��Ӧtd��ǩ��name����
        /// <td>Name</td>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ������,��Ӧtd��ǩ������
        /// <td>Data</td>
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// td���������ʽ����
        /// td class����
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// td ��ǩ��rowspan����ֵ
        /// </summary>
        public string Rowspan { get; set; }

        /// <summary>
        /// td ��ǩ��colspan����ֵ
        /// </summary>
        public string Colspan { get; set; }

        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// �Ƿ�ʹ��<code></code>��ǩ��ʾ����
        /// </summary>
        public string UseCode { get; set; }

        /// <summary>
        /// Դ�����Ƿ�ͨ��DES����,���Ϊ<code>true</code>,����Ҫ���ܺ���ʾ
        /// </summary>
        public string UseDES { get; set; }

        /// <summary>
        /// td��ǩ�����Ƿ�ʹ��<a></a>���ӱ�ʾ
        /// <td><a href=""></a></td>
        /// </summary>
        public string AsLink { get; set; }

        /// <summary>
        /// ���<see cref="AsLink"/>=true,��a��ǩhref����
        /// </summary>
        public string LinkHref { get; set; }

        public string LinkProperty { get; set; }

        public bool UsePopover { get; set; }
        public string PopoverTitle { get; set; }
        public string PopoverContent { get; set; }
        public string PopoverOptions { get; set; }

        /// <summary>
        /// td��ǩ����������
        /// </summary>
        public string Properties { get; set; }
    }
}