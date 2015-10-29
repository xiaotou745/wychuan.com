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

        public void AdjustBalance(BillDTO bill)
        {
            AssertUtils.ArgumentNotNull(bill, "bill");
           

            var billDetailType = (BillDetailType) Enum.Parse(typeof (BillDetailType), bill.DetailType.ToString());
            switch (billDetailType)
            {
                case BillDetailType.Income:
                    accountDao.AdjustBalance(bill.AccountId, bill.Price);
                    break;
                case BillDetailType.Expend:
                    accountDao.AdjustBalance(bill.AccountId, bill.Price*-1);
                    break;
                    //借贷分两种,一种是自己借出,一种是借入;
                case BillDetailType.Creditor:
                    if (bill.CreditorType == CreditType.JieChu.GetHashCode() ||
                        bill.CreditorType == CreditType.HuanKuan.GetHashCode())
                    {
                        accountDao.AdjustBalance(bill.AccountId, -1*bill.Price);
                    }
                    else
                    {
                        accountDao.AdjustBalance(bill.AccountId, bill.Price);
                    }
                    break;
                case BillDetailType.Transfer://转账
                    TransferPrice(bill.AccountId, bill.ToAccountId, bill.Price);
                    break;
            }
        }

        public void AdjustBalance(BillDTO bill, decimal price)
        {
            AssertUtils.ArgumentNotNull(bill, "bill");

            decimal adjustPrice = price;
            var billDetailType = (BillDetailType)Enum.Parse(typeof(BillDetailType), bill.DetailType.ToString());
            switch (billDetailType)
            {
                case BillDetailType.Income:
                case BillDetailType.Expend:
                //借贷分两种,一种是自己借出,一种是借入;
                case BillDetailType.Creditor:
                    accountDao.AdjustBalance(bill.AccountId, adjustPrice);
                    break;
                case BillDetailType.Transfer://转账
                    accountDao.AdjustBalance(bill.AccountId, adjustPrice);
                    accountDao.AdjustBalance(bill.ToAccountId, adjustPrice*-1);
                    break;
            }
        }

        public void TransferPrice(int fromAccountId, int toAccountId, decimal transferPrice)
        {
            AssertUtils.Greater(fromAccountId, 0);
            AssertUtils.Greater(toAccountId, 0);

            accountDao.AdjustBalance(fromAccountId, -1*transferPrice);
            accountDao.AdjustBalance(toAccountId, transferPrice);
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