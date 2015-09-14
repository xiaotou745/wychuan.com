using System;
using System.Text;
using System.Threading.Tasks;

namespace AC.Service.DTO.LiCai
{

    #region 明细/流水类型

    public class BillType
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }
    }

    #endregion

    public class AccountQueryInfo
    {
        public int? UserId { get; set; }

        public int? AccountTypeId { get; set; }

        public int? PageIndex { get; set; }
    }

    public class AccountDTO
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
        /// 账户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账户类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 账户类型名称
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 是否记入净资产
        /// </summary>
        public bool InNetAssets { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}