using System;
using System.Collections.Generic;
using AC.Dao;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
using AC.Service.LiCai;
using AC.Transaction.Common;

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
            var hasBalanceAdjust = billDao.HasBalanceAdjust(bill.AccountId, bill.ConsumeTime);
            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                if (!hasBalanceAdjust)
                {
                    var detailType = (BillDetailType)Enum.Parse(typeof(BillDetailType), bill.DetailType.ToString());
                    var creditType = CreditType.JieChu;
                    if (detailType == BillDetailType.Creditor)
                    {
                        creditType = (CreditType)Enum.Parse(typeof(CreditType), bill.CreditorType.ToString());
                    }

                    AdjustBalanceProvider
                        .Builder(detailType, bill.Price, bill.AccountId, bill.ToAccountId, creditType)
                        .Adjust();
                }
                int id = billDao.Insert(bill);
                unitOfWork.Complete();
                return id;
            }
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
            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                if (!hasBalanceAdjust)
                {
                    decimal adjustPrice = billInfo.Price - bill.Price;
                    var detailType = (BillDetailType)Enum.Parse(typeof(BillDetailType), billInfo.DetailType.ToString());
                    var creditType = CreditType.JieChu;
                    if (detailType == BillDetailType.Creditor)
                    {
                        creditType = (CreditType)Enum.Parse(typeof(CreditType), bill.CreditorType.ToString());
                    }

                    AdjustBalanceProvider
                        .Builder(detailType, adjustPrice, bill.AccountId, bill.ToAccountId, creditType)
                        .Adjust();
                }
                bill.UserId = billInfo.UserId;
                bill.BookId = billInfo.BookId;
                bill.IsBalanceAdjust = billInfo.IsBalanceAdjust;
                bill.DetailType = billInfo.DetailType;

                billDao.Update(bill);
                unitOfWork.Complete();
            }
        }

        /// <summary>
        /// 删除一条明细
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            var billInfo = billDao.GetById(id);
            //此之后是否有余额调整,如果此时间之后调整过余额，则账户余额不变，否则，账户余额更改；
            var hasBalanceAdjust = billDao.HasBalanceAdjust(billInfo.AccountId, billInfo.ConsumeTime);
            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                if (!hasBalanceAdjust)//如果之后此账户没有调整过余额，则需要把余额进行一下调整
                {
                    var detailType = (BillDetailType)Enum.Parse(typeof(BillDetailType), billInfo.DetailType.ToString());
                    var creditType = CreditType.JieChu;
                    if (detailType == BillDetailType.Creditor)
                    {
                        creditType = (CreditType)Enum.Parse(typeof(CreditType), billInfo.CreditorType.ToString());
                    }

                    AdjustBalanceProvider
                        .Builder(detailType, billInfo.Price, billInfo.AccountId, billInfo.ToAccountId, creditType)
                        .AdjustReverse();
                }
                billDao.Delete(id);
                unitOfWork.Complete();
            }
        }

        public BillDTO GetById(int id)
        {
            return billDao.GetById(id);
        }
    }
}