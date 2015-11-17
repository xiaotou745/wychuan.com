namespace wychuan2.com.Areas.admin.Models.User
{
    public class RolePrivilegeParams
    {
        public int RoleId { get; set; }

        public int[] MenuIds { get; set; }
    }
}