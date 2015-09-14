using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AC.Service.DTO.LiCai;

namespace wychuan2.com.Models
{
    public class LiCaiAccount
    {
        /// <summary>
        /// 账户类型
        /// </summary>
        public List<AccountType> AccountTypes { get; set; }

        /// <summary>
        /// 当前账户类型
        /// </summary>
        public AccountType CurrentType { get; set; }

        public IList<AccountDTO> Accounts { get; set; }
    }
}