using System;
using System.Collections.Generic;
using System.Linq;
using AC.Cache;
using AC.Dao.User;
using AC.Extension;
using AC.Service.DTO.User;
using AC.Service.Impl.Cache;
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

            int id = menuDao.Insert(menuDTO);
            UserCacheProvider.RefreshMenusInCache();
            return id;
        }

        public void Modify(MenuDTO menuDTO)
        {
            AssertUtils.ArgumentNotNull(menuDTO, "menu");

            menuDao.Update(menuDTO);

            UserCacheProvider.RefreshMenusInCache();
        }

        public void Remove(int id)
        {
            menuDao.Delete(id);

            UserCacheProvider.RefreshMenusInCache();
        }

        public MenuDTO GetById(int id)
        {
            var allMenus = GetAll();
            return allMenus.FirstOrDefault(m => m.Id == id);
        }

        public IList<MenuDTO> GetAll()
        {
            return UserCacheProvider.GetMenusInCache();
        }

        //public IList<MenuDTO> GetRolePrivilege(int roleId)
        //{
        //    var roleMenus = DataCache.GetCache(Consts.RolePrivilegeCacheKey.format(roleId)) as IList<MenuDTO>;
        //    if (roleMenus == null)
        //    {
        //        roleMenus = RefreshRolePrivilegeCache(roleId);
        //    }
        //    return roleMenus;
        //}

        #region Privite methods

        //private IList<MenuDTO> RefreshRolePrivilegeCache(int roleId)
        //{
        //    var roleMenus = menuDao.GetRolePrivilege(roleId);
        //    DataCache.SetCacheSliding(Consts.RolePrivilegeCacheKey.format(roleId), roleMenus, TimeSpan.FromDays(1));
        //    return roleMenus;
        //}
        #endregion
    }
}