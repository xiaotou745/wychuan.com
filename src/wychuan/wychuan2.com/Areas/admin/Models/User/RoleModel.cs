using System.Collections.Generic;
using AC.Service.DTO.User;

namespace wychuan2.com.Areas.admin.Models.User
{
    public class RoleModel
    {
        public IList<RoleDTO> Roles { get; set; }

        public RoleDTO CurrentRole { get; set; }

        public IList<MenuDTO> Menus { get; set; }
    }
}