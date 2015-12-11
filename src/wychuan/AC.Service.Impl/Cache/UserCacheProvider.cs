using System;
using System.Collections.Generic;
using System.Linq;
using AC.Cache;
using AC.Dao.User;
using AC.Extension;
using AC.Service.DTO.User;
using AC.Util;

namespace AC.Service.Impl.Cache
{
    public class UserCacheProvider
    {
        private static readonly MenuDao menuDao = new MenuDao();
        private static readonly UserDao userDao = new UserDao();

        #region 在缓存中的菜单

        public static IList<MenuDTO> GetMenusInCache()
        {
            var cacheMenus = DataCache.GetCache(Consts.MENU_CACHE_KEY) as IList<MenuDTO>;
            if (cacheMenus == null)
            {
                cacheMenus = menuDao.GetAll();
                DataCache.SetCacheSliding(Consts.MENU_CACHE_KEY, cacheMenus, TimeSpan.FromDays(1));
            }
            return cacheMenus;
        }

        public static IList<MenuDTO> RefreshMenusInCache()
        {
            IList<MenuDTO> cacheMenus = menuDao.GetAll();
            DataCache.SetCacheSliding(Consts.MENU_CACHE_KEY, cacheMenus, TimeSpan.FromDays(1));
            return cacheMenus;
        }

        #endregion

        #region 角色在缓存中的权限

        public static IList<int> GetRolePrivilegeInCache(int roleId)
        {
            var roleMenuIds = DataCache.GetCache(Consts.RolePrivilegeCacheKey.format(roleId)) as IList<int>;
            if (roleMenuIds == null)
            {
                var roleMenus = menuDao.GetRolePrivilege(roleId);
                roleMenuIds = roleMenus.Select(m => m.Id).ToList();
                DataCache.SetCacheSliding(Consts.RolePrivilegeCacheKey.format(roleId), roleMenuIds, TimeSpan.FromDays(1));
            }
            return roleMenuIds;
        }

        public static IList<int> RefreshRolePrivilegeInCache(int roleId)
        {
            var roleMenus = menuDao.GetRolePrivilege(roleId);
            var roleMenuIds = roleMenus.Select(m => m.Id).ToList();
            DataCache.SetCacheSliding(Consts.RolePrivilegeCacheKey.format(roleId), roleMenuIds, TimeSpan.FromDays(1));
            return roleMenuIds;
        }

        #endregion

        #region 已登录用户在缓存中的数据

        #region RefreshUserInCache 刷新缓存中的用户信息，返回用户菜单权限

        public static IList<int> RefreshUserInCache(int userId)
        {
            UserDTO userDTO = userDao.GetById(userId);
            return RefreshUserInCache(userDTO);
        }

        public static IList<int> RefreshUserInCache(UserDTO user)
        {
            if (user == null)
            {
                return new List<int>();
            }
            return RefreshUserInCache(user.Id, user.RoleIds);
        }

        public static IList<int> RefreshUserInCache(int userId, string strRoleIds)
        {
            strRoleIds = strRoleIds ?? string.Empty;
            string[] arrRoleIds = strRoleIds.Split(new string[] {","}, System.StringSplitOptions.RemoveEmptyEntries);
            List<int> roleIds = arrRoleIds.Select(r => ParseHelper.ToInt(r)).ToList();
            return RefreshUserInCache(userId, roleIds);
        }

        public static IList<int> RefreshUserInCache(int userId, List<int> roleIds)
        {
            //把用户所属角色信息更新到缓存
            SetUserRoleIdsInCache(userId, roleIds);
            //把用户权限更新到缓存
            return SetUserMenuIdsInCache(userId, roleIds);
        }

        #endregion

        #region UserExistsInCache

        public static bool UserExistsInCache(int userId)
        {
            Dictionary<int, List<int>> userRoleIdsInCache = GetUserRoleIdsInCache(); //已登录用户对应的角色
            //用户的权限
            var userMenuIds = DataCache.GetCache(Consts.UserPrivilegeCacheKey.format(userId)) as IList<int>;
            return userRoleIdsInCache.ContainsKey(userId) || userMenuIds != null;
        }

        #endregion

        #region RolePrivilegeChanged 角色权限更改之后需要更新缓存中用户的权限

        /// <summary>
        /// 用户所属角色不变
        /// 用户权限更新
        /// </summary>
        /// <param name="roleId"></param>
        public static void RolePrivilegeChanged(int roleId)
        {
            //先更新角色在缓存的中菜单权限
            RefreshRolePrivilegeInCache(roleId);
            //然后更新，属于此角色的用户在缓存中的权限
            Dictionary<int, List<int>> userRoleIdsInCache = GetUserRoleIdsInCache();
            foreach (var item in userRoleIdsInCache)
            {
                int userId = item.Key;
                if (item.Value.Contains(roleId)) //如果用户属于此角色，则此用户权限需要更改；
                {
                    SetUserMenuIdsInCache(userId, item.Value);
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 把用户权限更新到缓存
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        private static IList<int> SetUserMenuIdsInCache(int userId, List<int> roleIds)
        {
            IList<int> userMenuIds = new List<int>();
            if (roleIds != null)
            {
                foreach (var roleId in roleIds)
                {
                    var roleMenuIds = GetRolePrivilegeInCache(roleId);
                    foreach (var menuId in roleMenuIds)
                    {
                        if (!userMenuIds.Contains(menuId))
                        {
                            userMenuIds.Add(menuId);
                        }
                    }
                }
            }
            DataCache.SetCacheSliding(Consts.UserPrivilegeCacheKey.format(userId), userMenuIds, TimeSpan.FromDays(1));
            return userMenuIds;
        }

        /// <summary>
        /// 获取在缓存中的用户对应的角色ID信息
        /// </summary>
        /// <returns></returns>
        private static Dictionary<int, List<int>> GetUserRoleIdsInCache()
        {
            var users = DataCache.GetCache(Consts.USER_CACHE_KEY) as Dictionary<int, List<int>>;
            return users ?? new Dictionary<int, List<int>>();
        }

        /// <summary>
        /// 设置用户对应的角色到缓存中去；
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRoleIds"></param>
        /// <returns></returns>
        private static Dictionary<int, List<int>> SetUserRoleIdsInCache(int userId, List<int> userRoleIds)
        {
            var userInCache = GetUserRoleIdsInCache();
            if (userInCache.ContainsKey(userId))
            {
                userInCache[userId] = userRoleIds ?? new List<int>();
            }
            else
            {
                userInCache.Add(userId, userRoleIds ?? new List<int>());
            }
            DataCache.SetCacheSliding(Consts.USER_CACHE_KEY, userInCache, TimeSpan.FromDays(1));
            return userInCache;
        }

        #endregion

        #region GetUserMenuIdsInCache 获取用户权限

        /// <summary>
        /// 获取用户权限（从缓存中）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IList<int> GetUserMenuIdsInCache(int userId)
        {
            var userMenuIds = DataCache.GetCache(Consts.UserPrivilegeCacheKey.format(userId)) as IList<int>;
            if (userMenuIds == null)
            {
                userMenuIds = RefreshUserInCache(userId);
            }
            return userMenuIds;
        }

        #endregion

        #region ClearUser 注销之后，清空缓存中的用户信息

        public static void ClearUserInCache(int userId)
        {
            //清空用户所属角色缓存
            Dictionary<int, List<int>> userRoleIdsInCache = GetUserRoleIdsInCache();
            if (userRoleIdsInCache.ContainsKey(userId))
            {
                userRoleIdsInCache.Remove(userId);
                DataCache.SetCacheSliding(Consts.USER_CACHE_KEY, userRoleIdsInCache, TimeSpan.FromDays(1));
            }

            //清空用户权限
            var userMenuIds = DataCache.GetCache(Consts.UserPrivilegeCacheKey.format(userId)) as IList<int>;
            if (userMenuIds != null)
            {
                DataCache.Remove(Consts.UserPrivilegeCacheKey.format(userId));
            }
        }

        #endregion

        #endregion
    }
}