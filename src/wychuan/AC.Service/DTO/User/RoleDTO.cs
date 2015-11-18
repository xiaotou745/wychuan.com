namespace AC.Service.DTO.User
{
    public class RoleDTO
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 此角色是否可以删除
        /// </summary>
        public bool CanRemove { get; set; }
    }
}