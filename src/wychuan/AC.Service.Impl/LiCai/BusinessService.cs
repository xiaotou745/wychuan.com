using System;
using System.Collections.Generic;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
using AC.Service.LiCai;
using AC.Util;

namespace AC.Service.Impl.LiCai
{
    public class BusinessService : IBusinessService
    {
        private readonly BusinessDao businessDao;

        public BusinessService()
        {
            businessDao = new BusinessDao();
        }

        public int Create(BusinessDTO business)
        {
            AssertUtils.ArgumentNotNull(business, "business");

            return businessDao.Insert(business);
        }

        public IList<BusinessDTO> GetByUserId(int userId)
        {
            return businessDao.GetByUserId(userId);
        }
    }
}