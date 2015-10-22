using System;
using System.Collections.Generic;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
using AC.Service.LiCai;

namespace AC.Service.Impl.LiCai
{
    public class BillTemplateService : IBillTemplateService
    {
        private readonly BillTemplateDao dao;

        public BillTemplateService()
        {
            this.dao = new BillTemplateDao();
        }

        public int Create(BillTemplateDTO lCBillTemplateDTO)
        {
            return dao.Insert(lCBillTemplateDTO);
        }

        public void Modify(BillTemplateDTO billTemplate)
        {
            dao.Update(billTemplate);
        }

        public void Remove(int id)
        {
            dao.Delete(id);
        }

        public IList<BillTemplateDTO> GetByUserId(int userId)
        {
            return dao.GetByUserId(userId);
        }
    }
}