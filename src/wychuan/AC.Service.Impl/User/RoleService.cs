using System.Collections.Generic;
using System.Linq;
using AC.Dao;
using AC.Dao.User;
using AC.Service.DTO.User;
using AC.Service.User;
using AC.Transaction.Common;

namespace AC.Service.Impl.User
{
    public class RoleService : IRoleService
    {
        private readonly RoleDao roleDao;

        public RoleService()
        {
            roleDao = new RoleDao();
        }

        public int Create(RoleDTO roleDTO)
        {
            return roleDao.Insert(roleDTO);
        }

        public void Modify(RoleDTO role)
        {
            roleDao.Update(role);
        }

        public void Remove(int id)
        {
            roleDao.Delete(id);
        }

        public IList<RoleDTO> GetAll()
        {
            return roleDao.GetAll();
        }

        public void DeletePrivilege(int roleId)
        {
            roleDao.DeletePrivilege(roleId);
        }

        public void SavePrivilege(int roleId, int[] menuIds)
        {
            using (IUnitOfWork unitOfWork = UtilOfWorkFactoryProvider.GetUnitOfWork())
            {
                roleDao.DeletePrivilege(roleId);
                foreach (int menuId in menuIds)
                {
                    roleDao.SavePrivilege(roleId, menuId);
                }
                unitOfWork.Complete();
            }
        }

        public int[] GetPrivilege(int roleId)
        {
            return roleDao.GetPrivilege(roleId).ToArray();
        }
    }
}