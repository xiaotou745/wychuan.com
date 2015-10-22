using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.LiCai;

namespace AC.Service.LiCai
{
    public interface IBillTemplateService
    {
        /// <summary>
        /// 新增一条记录
        ///<param name="lCBillTemplateDTO">要新增的对象</param>
        /// </summary>
        int Create(BillTemplateDTO lCBillTemplateDTO);

        void Modify(BillTemplateDTO billTemplate);

        void Remove(int id);

        IList<BillTemplateDTO> GetByUserId(int userId);
    }
}
