using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.LiCai;

namespace AC.Service.LiCai
{
    public interface IBillService
    {
        /// <summary>
        /// 新增账单
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        int Create(BillDTO bill);

        /// <summary>
        /// 账单查询
        /// </summary>
        /// <returns></returns>
        IList<BillDTO> Query(BillQueryInfo queryInfo);
    }
}
