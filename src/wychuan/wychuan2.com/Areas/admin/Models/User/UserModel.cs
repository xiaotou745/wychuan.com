﻿using System.Collections.Generic;
using AC.Service.DTO.User;

namespace wychuan2.com.Areas.admin.Models.User
{
    public class UserModel
    {
        public IList<UserDTO> Users { get; set; }

        public IList<RoleDTO> Roles { get; set; }
    }
}