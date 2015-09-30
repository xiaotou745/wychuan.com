namespace wychuan2.com.Areas.admin.Models
{
    /// <summary>
    /// td数据项
    /// </summary>
    public class TableRowItem
    {
        /// <summary>
        /// 名称,对应td标签的name属性
        /// <td>Name</td>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 内容项,对应td标签的内容
        /// <td>Data</td>
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// td数据项的样式属性
        /// td class属性
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// td 标签的rowspan属性值
        /// </summary>
        public string Rowspan { get; set; }

        /// <summary>
        /// td 标签的colspan属性值
        /// </summary>
        public string Colspan { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 是否使用<code></code>标签显示内容
        /// </summary>
        public string UseCode { get; set; }

        /// <summary>
        /// 源数据是否通过DES加密,如果为<code>true</code>,则需要解密后显示
        /// </summary>
        public string UseDES { get; set; }

        /// <summary>
        /// td标签内容是否使用<a></a>链接表示
        /// <td><a href=""></a></td>
        /// </summary>
        public string AsLink { get; set; }

        /// <summary>
        /// 如果<see cref="AsLink"/>=true,则a标签href属性
        /// </summary>
        public string LinkHref { get; set; }

        public string LinkProperty { get; set; }

        public bool UsePopover { get; set; }
        public string PopoverTitle { get; set; }
        public string PopoverContent { get; set; }
        public string PopoverOptions { get; set; }

        /// <summary>
        /// td标签的其它属性
        /// </summary>
        public string Properties { get; set; }
    }
}