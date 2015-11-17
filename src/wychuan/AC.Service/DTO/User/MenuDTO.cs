namespace AC.Service.DTO.User
{
    public class MenuDTO
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 菜单URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderBy { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }
    }
}