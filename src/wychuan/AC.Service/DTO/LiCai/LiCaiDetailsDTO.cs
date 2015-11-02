using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Service.DTO.LiCai
{
    /// <summary>
    /// 实体类LCLiCaiDetailsDTO 。(属性说明自动提取数据库字段的描述信息)
    /// Generate By: mywww2.wychuan.com
    /// Generate Time: 2015-11-02 19:24:17
    /// </summary>
    public class LiCaiDetailsDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 购买账户
        /// </summary>
        public int AccountId { get; set; }

        public string Account { get; set; }

        /// <summary>
        /// 买入的项目
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime BuyDay { get; set; }

        /// <summary>
        /// 多长时间
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 单位(天、月、年)
        /// </summary>
        public string TimeUnit { get; set; }

        /// <summary>
        /// 买入金额
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 年息比率(10%)
        /// </summary>
        public decimal InterestRate { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime? ExpireDay { get; set; }

        /// <summary>
        /// 实际赎回日期
        /// </summary>
        public DateTime? RedeemDay { get; set; }

        /// <summary>
        /// 实际赎回金额
        /// </summary>
        public decimal RedeemPrice { get; set; }

        /// <summary>
        /// 预期收益
        /// </summary>
        public decimal ExpirePrice
        {
            get
            {
                decimal result = 0;
                if (TimeUnit == "天")
                {
                    result = 10000 * (InterestRate / 100) * (Price / 10000) * ((decimal)Times / 365);// ((decimal) Times/365)/10000;
                }
                else if(TimeUnit == "月")
                {
                    result = 10000 * (InterestRate / 100) * (Price / 10000) * ((decimal)Times / 12);
                }
                else if (TimeUnit == "年")
                {
                    result = 10000 * (InterestRate / 100) * (Price / 10000) * Times;
                }
                return result;
            }
        }
    }
}