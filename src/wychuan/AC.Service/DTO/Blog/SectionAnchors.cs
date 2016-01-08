namespace AC.Service.DTO.Blog
{
    /// <summary>
    /// 实体类SectionAnchors 。(属性说明自动提取数据库字段的描述信息)
    /// Generate By: wangyuchuan
    /// Generate Time: 2016-01-07 17:06:46
    /// </summary>
    public class SectionAnchors
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 段落ID
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        /// 锚点Id
        /// </summary>
        public string AnchorId { get; set; }

        /// <summary>
        /// 锚点文本
        /// </summary>
        public string AnchorText { get; set; }
    }
}