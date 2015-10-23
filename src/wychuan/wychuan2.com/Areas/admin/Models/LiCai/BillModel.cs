using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AC.Service.DTO.LiCai;

namespace wychuan2.com.Areas.admin.Models.LiCai
{
    public class BillModel
    {
        public IList<ItemDTO> Items { get; set; }

        /// <summary>
        /// 账户列表
        /// </summary>
        public IList<AccountDTO> Accounts { get; set; }

        /// <summary>
        /// 分类列表
        /// </summary>
        public IList<CategoryDTO> Categories { get; set; }

        public IList<BillTemplateDTO> Templates { get; set; }

        public IList<BillDTO> Details { get; set; }
    }
}