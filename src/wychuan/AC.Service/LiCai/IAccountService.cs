using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.LiCai;

namespace AC.Service.LiCai
{
    public interface IAccountService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        IList<AccountDTO> Query(AccountQueryInfo queryInfo);

        /// <summary>
        /// 创建账户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        int Create(AccountDTO account);

        void Modify(AccountDTO account);

        void Remove(int id);

        Dictionary<int, int> GetTypeCounts();
    }
}
