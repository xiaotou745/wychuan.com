using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Service.DTO.LiCai
{
    /// <summary>
    /// 实体类LCBillTemplateDTO 。(属性说明自动提取数据库字段的描述信息)
    /// Generate By: tools.wychuan.com
    /// Generate Time: 2015-10-21 15:16:09
    /// </summary>
    public class BillTemplateDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 账本ID
        /// </summary>
        public int BookId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int FirstCategoryId { get; set; }
        /// <summary>
        /// 一级类目
        /// </summary>
        public string FirstCategory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SecondCategoryId { get; set; }
        /// <summary>
        /// 二级类目
        /// </summary>
        public string SecondCategory { get; set; }
        /// <summary>
        /// 账户ID
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// 账户文本
        /// </summary>
        public string AccountDesc { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int DetailType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BusinessId { get; set; }
        /// <summary>
        /// 支出商家
        /// </summary>
        public string Business { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MemberId { get; set; }
        /// <summary>
        /// 收入或支出成员
        /// </summary>
        public string Member { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// 收入或支出项目
        /// </summary>
        public string Project { get; set; }
        /// <summary>
        /// 报销状态（0 不报销 1 待报销 2 已报销)
        /// </summary>
        public int BaoXiao { get; set; }
        /// <summary>
        /// 是否有效(0无效 1有效)
        /// </summary>
        public bool Enable { get; set; }


    }

}
