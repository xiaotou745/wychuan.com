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
        /// 账本
        /// </summary>
        //public IList<BillBooksDTO> Books { get; set; }

        /// <summary>
        /// 成员
        /// </summary>
        //public IList<MemberDTO> Members { get; set; }

        /// <summary>
        /// 账户列表
        /// </summary>
        public IList<AccountDTO> Accounts { get; set; }

        /// <summary>
        /// 分类列表
        /// </summary>
        public IList<CategoryDTO> Categories { get; set; }

        /// <summary>
        /// 商家列表
        /// </summary>
        //public IList<BusinessDTO> Business { get; set; }

        /// <summary>
        /// 项目列表
        /// </summary>
        //public IList<ProjectDTO> Projects { get; set; }
    }
}