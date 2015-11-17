using System;
using System.Collections.Generic;

namespace AC.Service.DTO.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string LoginPassword { get; set; }
        public DateTime RegisterTime { get; set; }

        public bool IsDisable { get; set; }

        public string RoleIds { get; set; }

        public IList<string> Roles { get; set; }
    }
}