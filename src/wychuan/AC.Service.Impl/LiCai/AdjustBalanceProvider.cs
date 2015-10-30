using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.LiCai;
using AC.Service.LiCai;

namespace AC.Service.Impl.LiCai
{
    public class AdjustBalanceProvider
    {
        private readonly IAccountService accountService = new AccountService();

        private AdjustBalanceProvider()
        {
        }

        private BillDetailType billType;
        private decimal adjustPrice;
        private int accountId;
        private int toAccountId;

        public static AdjustBalanceProvider Builder(BillDetailType detailType, decimal price, int accountId,
            int toAccountId = 0, CreditType creditType = CreditType.JieChu)
        {
            var absPrice = Math.Abs(price);

            var provider = new AdjustBalanceProvider
            {
                billType = detailType,
                accountId = accountId,
                toAccountId = toAccountId
            };
            switch (detailType)
            {
                case BillDetailType.Income: //收入是加钱
                    provider.adjustPrice = absPrice;
                    break;
                case BillDetailType.Transfer: //转账是减钱
                case BillDetailType.Expend: //支出是减钱
                    provider.adjustPrice = -1*absPrice;
                    break;
                case BillDetailType.Creditor: //借贷需要根据类型来决定是加还是减
                    //借出、还款都是给他人钱，所以是减
                    if (creditType == CreditType.JieChu || creditType == CreditType.HuanKuan)
                    {
                        provider.adjustPrice = -1*absPrice;
                    }
                    else
                    {
                        provider.adjustPrice = absPrice;
                    }
                    break;
            }
            return provider;
        }

        /// <summary>
        /// 调整余额
        /// </summary>
        public void Adjust()
        {
            //如果是转账
            if (billType == BillDetailType.Transfer)
            {
                accountService.AdjustBalance(accountId, adjustPrice);
                accountService.AdjustBalance(toAccountId, -1*adjustPrice);
            }
            else
            {
                accountService.AdjustBalance(accountId, adjustPrice);
            }
        }

        /// <summary>
        /// 反方向调整,删除明细时
        /// </summary>
        public void AdjustReverse()
        {
            //如果是转账
            if (billType == BillDetailType.Transfer)
            {
                accountService.AdjustBalance(accountId, -1 * adjustPrice);
                accountService.AdjustBalance(toAccountId,  adjustPrice);
            }
            else
            {
                accountService.AdjustBalance(accountId, -1 * adjustPrice);
            }
        }
    }
}