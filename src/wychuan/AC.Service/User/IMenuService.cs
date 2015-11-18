using System.Collections.Generic;
using AC.Service.DTO.User;

namespace AC.Service.User
{
    public interface IMenuService
    {
        /// <summary>
        /// 新增一条记录
        ///<param name="menuDTO">要新增的对象</param>
        /// </summary>
        int Create(MenuDTO menuDTO);

        /// <summary>
        /// 修改一条记录
        ///<param name="menuDTO">要修改的对象</param>
        /// </summary>
        void Modify(MenuDTO menuDTO);

        /// <summary>
        /// 删除一条记录
        /// </summary>
        void Remove(int id);

        /// <summary>
        /// 根据Id得到一个对象实体
        /// </summary>
        MenuDTO GetById(int id);

        /// <summary>
        /// 查询方法
        /// </summary>
        IList<MenuDTO> GetAll();

        ///// <summary>
        ///// 获取指定角色的菜单权限
        ///// </summary>
        ///// <param name="roleId"></param>
        ///// <returns></returns>
        //IList<MenuDTO> GetRolePrivilege(int roleId);
    }
}