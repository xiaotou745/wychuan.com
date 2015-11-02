using System.Collections.Generic;
using AC.Service.DTO.LiCai;

namespace wychuan2.com.Areas.admin.Models.LiCai
{
    public class LiCaiModel
    {
        /// <summary>
        /// 账户列表
        /// </summary>
        public IList<AccountDTO> Accounts { get; set; }

        /// <summary>
        /// 理财明细
        /// </summary>
        public IList<LiCaiDetailsDTO> LiCaiDetails { get; set; }
    }
}