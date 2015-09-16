using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Service.DTO.LiCai
{
    public enum ItemType
    {
        /// <summary>
        /// 账本
        /// </summary>
        BillBook = 0,

        /// <summary>
        /// 成员
        /// </summary>
        Member = 1,

        /// <summary>
        /// 项目
        /// </summary>
        Project = 2,

        /// <summary>
        /// 商家
        /// </summary>
        Business = 3,

        /// <summary>
        /// 付款方
        /// </summary>
        PaymentPerson = 4,

        /// <summary>
        /// 债权人
        /// </summary>
        Creditor = 5,

        /// <summary>
        /// 收支分类
        /// </summary>
        Category = 6,
    }

    public class ItemDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户ID，如果为0，则为默认数据
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 1、成员 2、项目
        /// </summary>
        public ItemType Type { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否常用级别 0常用  1不常用 数字超小，优先级越高
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// 最后一次使用时间
        /// </summary>
        public DateTime? LastUseTime { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEnable { get; set; }
    }
}