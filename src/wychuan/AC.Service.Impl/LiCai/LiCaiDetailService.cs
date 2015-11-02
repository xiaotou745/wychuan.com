using System;
using System.Collections.Generic;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
using AC.Service.LiCai;

namespace AC.Service.Impl.LiCai
{
    public class LiCaiDetailService : ILiCaiDetailService
    {
        private readonly LiCaiDao licaiDao;

        public LiCaiDetailService()
        {
            licaiDao = new LiCaiDao();
        }

        public int Create(LiCaiDetailsDTO licaiDetail)
        {
            return licaiDao.Insert(licaiDetail);
        }

        public IList<LiCaiDetailsDTO> GetByUserId(int userId)
        {
            return licaiDao.GetByUserId(userId);
        }
    }
}