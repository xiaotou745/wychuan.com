using System;
using System.Collections.Generic;
using System.Linq;
using AC.Cache;
using AC.Dao.LiCai;
using AC.Extension;
using AC.Service.DTO.LiCai;

namespace AC.Service.Impl.Cache
{
    public class LiCaiCacheProvider
    {
        private static readonly CategoryDao categoryDao = new CategoryDao();
        private static readonly ItemsDao itemsDao = new ItemsDao();
        private static readonly AccountDao accountDao = new AccountDao();

        #region Category

        public static IList<CategoryDTO> GetCategoriesInCache(int userId)
        {
            var cacheCategories =
                DataCache.GetCache(Consts.UserLiCaiCategoryCacheKey.format(userId)) as IList<CategoryDTO>;
            if (cacheCategories == null)
            {
                cacheCategories = categoryDao.GetByUserId(userId);
                DataCache.SetCacheSliding(Consts.UserLiCaiCategoryCacheKey.format(userId), cacheCategories,
                    TimeSpan.FromDays(3));
            }
            return cacheCategories;
        }

        public static IList<CategoryDTO> RefreshCategoriesInCache(int userId)
        {
            var cacheCategories = categoryDao.GetByUserId(userId);
            DataCache.SetCacheSliding(Consts.UserLiCaiCategoryCacheKey.format(userId), cacheCategories,
                TimeSpan.FromDays(3));
            return cacheCategories;
        }

        #endregion

        #region Items
        public static IList<ItemDTO> GetItemsInCache(int userId)
        {
            var cacheItems = DataCache.GetCache(Consts.UserLiCaiItemsCacheKey.format(userId)) as IList<ItemDTO>;
            if (cacheItems == null)
            {
                cacheItems = itemsDao.GetByUserId(userId);
                DataCache.SetCacheSliding(Consts.UserLiCaiItemsCacheKey.format(userId), cacheItems, TimeSpan.FromDays(3));
            }
            return cacheItems;
        }

        public static IList<ItemDTO> RefreshItemsInCache(int userId)
        {
            var cacheItems = itemsDao.GetByUserId(userId);
            DataCache.SetCacheSliding(Consts.UserLiCaiItemsCacheKey.format(userId), cacheItems, TimeSpan.FromDays(3));
            return cacheItems;
        }
        #endregion

        #region Account
        public static IList<AccountDTO> GetAccountsInCache(int userId)
        {
            var cacheAccounts = DataCache.GetCache(Consts.UserLiCaiAccountsCacheKey.format(userId)) as IList<AccountDTO>;
            if (cacheAccounts == null)
            {
                cacheAccounts = accountDao.Query(new AccountQueryInfo { UserId = userId,IsEnable=null });
                List<AccountType> lstAccountTypes = AccountType.Create().GetAccountTypes();
                foreach (var account in cacheAccounts)
                {
                    account.AccountType = lstAccountTypes.First(a => a.Id == account.Type).Name;
                }
                DataCache.SetCacheSliding(Consts.UserLiCaiAccountsCacheKey.format(userId), cacheAccounts, TimeSpan.FromDays(3));
            }
            return cacheAccounts;
        }

        public static IList<AccountDTO> RefreshAccountsInCache(int userId)
        {
            var cacheAccounts = accountDao.Query(new AccountQueryInfo { UserId = userId, IsEnable = null });
            List<AccountType> lstAccountTypes = AccountType.Create().GetAccountTypes();
            foreach (var account in cacheAccounts)
            {
                account.AccountType = lstAccountTypes.First(a => a.Id == account.Type).Name;
            }

            DataCache.SetCacheSliding(Consts.UserLiCaiAccountsCacheKey.format(userId), cacheAccounts, TimeSpan.FromDays(3));
            return cacheAccounts;
        }
        #endregion
    }
}