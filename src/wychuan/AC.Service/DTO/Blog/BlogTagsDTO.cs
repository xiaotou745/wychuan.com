namespace AC.Service.DTO.Blog
{
    public class BlogTagsDTO
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 标签文本
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 是否有效(0无效1有效)
        /// </summary>
        public bool IsEnable { get; set; }
    }
}