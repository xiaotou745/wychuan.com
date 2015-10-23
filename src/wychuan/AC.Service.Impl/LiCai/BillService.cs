using System;
using System.Collections.Generic;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
using AC.Service.LiCai;

namespace AC.Service.Impl.LiCai
{
    public class BillService : IBillService
    {
        private BillDao billDao;
        public BillService()
        {
            billDao = new BillDao();
        }

        public int Create(BillDTO bill)
        {
            return billDao.Insert(bill);
        }

        public IList<BillDTO> Query(BillQueryInfo queryInfo)
        {
            return billDao.Query(queryInfo);
        }
    }
}