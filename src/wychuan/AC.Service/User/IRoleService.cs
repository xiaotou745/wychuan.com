using System.Collections.Generic;
using AC.Service.DTO.User;

namespace AC.Service.User
{
    public interface IRoleService
    {
        /// <summary>
        /// 新增一条记录
        ///<param name="roleDTO">要新增的对象</param>
        /// </summary>
        int Create(RoleDTO roleDTO);

        void Modify(RoleDTO role);

        void Remove(int id);

        IList<RoleDTO> GetAll();

        void DeletePrivilege(int roleId);

        void SavePrivilege(int roleId, int[] menuIds);

        int[] GetPrivilege(int roleId);
    }
}