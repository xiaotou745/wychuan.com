using System.Collections.Generic;
using AC.Dao.User;
using AC.Service.DTO.User;
using AC.Service.User;
using AC.Util;

namespace AC.Service.Impl.User
{
    public class MenuService : IMenuService
    {
        private readonly MenuDao menuDao;

        public MenuService()
        {
            menuDao = new MenuDao();
        }

        public int Create(MenuDTO menuDTO)
        {
            AssertUtils.ArgumentNotNull(menuDTO, "menu");

            return menuDao.Insert(menuDTO);
        }

        public void Modify(MenuDTO menuDTO)
        {
            AssertUtils.ArgumentNotNull(menuDTO, "menu");

            menuDao.Update(menuDTO);
        }

        public void Remove(int id)
        {
            menuDao.Delete(id);
        }

        public MenuDTO GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<MenuDTO> GetAll()
        {
            return menuDao.GetAll();
        }

        public IList<MenuDTO> GetRolePrivilege(int roleId)
        {
            return menuDao.GetRolePrivilege(roleId);
        }
    }
}