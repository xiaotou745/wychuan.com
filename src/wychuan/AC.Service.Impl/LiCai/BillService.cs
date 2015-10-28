using System;
using System.Collections.Generic;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
using AC.Service.LiCai;

namespace AC.Service.Impl.LiCai
{
    public class BillService : IBillService
    {
        private readonly BillDao billDao;
        private readonly IAccountService accountService;
        public BillService()
        {
            billDao = new BillDao();
            accountService = new AccountService();
        }

        public int Create(BillDTO bill)
        {
            //此之后是否有余额调整,如果此时间之后调整过余额，则账户余额不变，否则，账户余额更改；
            var hasBalanceAdjust = billDao.HasBalanceAdjust(bill.UserId, bill.ConsumeTime);
            if (!hasBalanceAdjust)
            {
                accountService.AdjustBalance(bill);
            }
            return billDao.Insert(bill);
        }

        public IList<BillDTO> Query(BillQueryInfo queryInfo)
        {
            return billDao.Query(queryInfo);
        }
    }

    public abstract class AbstractBillCreator
    {
        public abstract void DealBalance();
    }


}