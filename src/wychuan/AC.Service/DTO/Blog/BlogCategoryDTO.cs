namespace AC.Service.DTO.Blog
{
    public class BlogCategoryDTO
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
        /// 父类型ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 有效标记
        /// </summary>
        public bool IsEnable { get; set; }
    }
}