using System;
using System.Collections.Generic;
using System.Linq;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
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

            return accountDao.Insert(account);
        }

        public void Modify(AccountDTO account)
        {
            AssertUtils.ArgumentNotNull(account, "account");
            AssertUtils.Greater(account.UserId, 0);

            accountDao.Update(account);
        }

        public void Remove(int id)
        {
            AssertUtils.Greater(id, 0);

            accountDao.Delete(id);
        }

        public Dictionary<int, int> GetTypeCounts(int userId)
        {
            Dictionary<int, int> typeCounts = accountDao.GetTypeCounts(userId);
            typeCounts.Add(0, typeCounts.Values.Sum());
            return typeCounts;
        }
    }
}