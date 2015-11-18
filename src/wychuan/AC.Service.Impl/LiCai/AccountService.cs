using System;
using System.Collections.Generic;
using System.Linq;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
using AC.Service.Impl.Cache;
using AC.Service.LiCai;
using AC.Util;

namespace AC.Service.Impl.LiCai
{
    public class AccountService : IAccountService
    {
        private readonly AccountDao accountDao;
        public AccountService()
        {
            accountDao = new AccountDao();
        }

        /// <summary>
        /// 账户
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public IList<AccountDTO> Query(AccountQueryInfo queryInfo)
        {
            IList<AccountDTO> userAccounts = LiCaiCacheProvider.GetAccountsInCache(queryInfo.UserId.Value);
            if (queryInfo.IsEnable.HasValue)
            {
                userAccounts = userAccounts.Where(a => a.IsEnable == queryInfo.IsEnable.Value).ToList();
            }
            if (queryInfo.AccountTypeId.HasValue)
            {
                userAccounts = userAccounts.Where(a => a.Type == queryInfo.AccountTypeId.Value).ToList();
            }
            return userAccounts;
            //IList<AccountDTO> lstAccounts = accountDao.Query(queryInfo);
            //List<AccountType> lstAccountTypes = AccountType.Create().GetAccountTypes();
            //foreach (var account in lstAccounts)
            //{
            //    account.AccountType = lstAccountTypes.First(a => a.Id == account.Type).Name;
            //}
            //return lstAccounts;
        }

        public IList<AccountDTO> Search(AccountQueryInfo queryInfo)
        {
            IList<AccountDTO> lstAccounts = accountDao.Query(queryInfo);
            List<AccountType> lstAccountTypes = AccountType.Create().GetAccountTypes();
            foreach (var account in lstAccounts)
            {
                account.AccountType = lstAccountTypes.First(a => a.Id == account.Type).Name;
            }
            return lstAccounts;
        }

        /// <summary>
        /// 创建账户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int Create(AccountDTO account)
        {
            AssertUtils.ArgumentNotNull(account, "account");
            AssertUtils.Greater(account.UserId, 0);

            var id = accountDao.Insert(account);
            LiCaiCacheProvider.RefreshAccountsInCache(account.UserId);
            return id;
        }

        public void Modify(AccountDTO account)
        {
            AssertUtils.ArgumentNotNull(account, "account");
            AssertUtils.Greater(account.UserId, 0);

            accountDao.Update(account);
            LiCaiCacheProvider.RefreshAccountsInCache(account.UserId);
        }

        public void AdjustBalance(int accountId, decimal price)
        {
            accountDao.AdjustBalance(accountId, price);
        }

        public void Remove(int id)
        {
            AssertUtils.Greater(id, 0);

            AccountDTO account = accountDao.GetById(id);
            accountDao.Delete(id);
            LiCaiCacheProvider.RefreshAccountsInCache(account.UserId);
        }

        public Dictionary<int, int> GetTypeCounts(int userId)
        {
            Dictionary<int, int> typeCounts = accountDao.GetTypeCounts(userId);
            typeCounts.Add(0, typeCounts.Values.Sum());
            return typeCounts;
        }

        public AccountDTO GetById(int id)
        {
            return accountDao.GetById(id);
        }
    }
}