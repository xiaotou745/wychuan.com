using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Service.DTO.LiCai
{
    public enum BillDetailType
    {
        /// <summary>
        /// 收入
        /// </summary>
        [Description("收入")]
        Income = 2,
        /// <summary>
        /// 支出
        /// </summary>
        [Description("支出")]
        Expend = 1,

        [Description("转账")]
        Transfer=3,

        [Description("借贷")]
        Creditor=4,
    }
    public class BillDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 账本ID
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 支出/收入分类ID
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// 账户ID/借入账户/借出账户
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 转账使用，目标账户ID
        /// </summary>
        public int ToAccountId { get; set; }

        /// <summary>
        /// 消费时间
        /// </summary>
        public DateTime ConsumeTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int DetailType { get; set; }

        /// <summary>
        /// 收入付款方或支出商家或借出债务人或借入债权人
        /// </summary>
        public string Business { get; set; }

        /// <summary>
        /// 收入或支出成员
        /// </summary>
        public string Member { get; set; }

        /// <summary>
        /// 收入或支出项目
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// 借入、借出、收款、还款
        /// </summary>
        public string CreditorType { get; set; }

        /// <summary>
        /// 还款是否需要提醒
        /// </summary>
        public string RefundNotice { get; set; }

        /// <summary>
        /// 还款日期
        /// </summary>
        public DateTime? RefundTime { get; set; }

        /// <summary>
        /// 报销状态（0 不报销 1 待报销 2 已报销)
        /// </summary>
        public int BaoXiao { get; set; }
    }
}