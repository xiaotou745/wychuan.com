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

        public void Modify(BillDTO bill)
        {
            var billInfo = billDao.GetById(bill.ID);
            //此之后是否有余额调整,如果此时间之后调整过余额，则账户余额不变，否则，账户余额更改；
            var hasBalanceAdjust = billDao.HasBalanceAdjust(bill.UserId, bill.ConsumeTime);
            if (!hasBalanceAdjust)
            {
                decimal adjustPrice = billInfo.Price - bill.Price;
                accountService.AdjustBalance(bill, adjustPrice);
            }
            bill.UserId = billInfo.UserId;
            bill.BookId = billInfo.BookId;
            bill.IsBalanceAdjust = billInfo.IsBalanceAdjust;
            bill.DetailType = billInfo.DetailType;
            
            billDao.Update(bill);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public BillDTO GetById(int id)
        {
            return billDao.GetById(id);
        }
    }
}