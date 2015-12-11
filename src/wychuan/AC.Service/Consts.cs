namespace AC.Service
{
    public class Consts
    {
        #region 权限 菜单相关
        /// <summary>
        /// 所有菜单缓存KEY
        /// </summary>
        public const string MENU_CACHE_KEY = "AdminMenuCacheKey";
        /// <summary>
        /// 每个角色缓存KEY
        /// </summary>
        public static string RolePrivilegeCacheKey = "RolePrivilegeCacheKey_{0}";

        /// <summary>
        /// 已经记录了权限的用户
        /// </summary>
        public const string USER_CACHE_KEY = "UserCacheKey";

        /// <summary>
        /// 每个用户的权限缓存KEY
        /// </summary>
        public static string UserPrivilegeCacheKey = "UserPrivilegeCacheKey_{0}";
        #endregion

        #region 理财相关
        /// <summary>
        /// 用户理财类目
        /// </summary>
        public static string UserLiCaiCategoryCacheKey = "UserLiCaiCategoryCacheKey_{0}";

        /// <summary>
        /// 用户理财基础数据缓存KEY
        /// </summary>
        public static string UserLiCaiItemsCacheKey = "UserLiCaiItemsCacheKey_{0}";
        /// <summary>
        /// 账户
        /// </summary>
        public static string UserLiCaiAccountsCacheKey = "UserLiCaiAccountsCacheKey_{0}";

        #endregion

        #region Cookie
        #endregion
    }
}
