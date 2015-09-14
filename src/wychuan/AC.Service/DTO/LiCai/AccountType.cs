using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AC.Helper;

namespace AC.Service.DTO.LiCai
{
    /// <summary>
    /// 账户类型
    /// </summary>
    public class AccountType
    {
        private string filePath;
        private List<AccountType> lstAccountTypes;
        private static AccountType instance;

        private AccountType()
        {
            
        }

        public static AccountType Create(string filePath, bool includeAll)
        {
            if (instance == null)
            {
                instance = new AccountType {filePath = filePath};
                instance.Init(includeAll);
            }
            return instance;
        }

        public static AccountType Create()
        {
            if (instance == null)
            {
                throw new Exception("AccountType is not init yet.");
            }
            return instance;
        }

        public List<AccountType> GetAccountTypes()
        {
            return instance.lstAccountTypes;
        }

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 账户数量
        /// </summary>
        public int Count { get; set; }

        private void Init(bool includeAll)
        {
            if (!File.Exists(filePath))
            {
                XmlHelper.ToXml(filePath, GetDefault());
            }
            lstAccountTypes = XmlHelper.Deserialize<AccountType[]>(filePath).ToList();
            if (includeAll)
            {
                lstAccountTypes.Insert(0, new AccountType {Id = 0, Name = "全部", Description = ""});
            }
        }

        private AccountType[] GetDefault()
        {
            var result = new List<AccountType>()
            {
                new AccountType {Id = 1, Name = "现金", Description = ""},
                new AccountType {Id = 2, Name = "储蓄卡", Description = ""},
                new AccountType {Id = 3, Name = "信用卡", Description = ""},
                new AccountType {Id = 4, Name = "投资账户", Description = "股票、P2P之类"},
                new AccountType {Id = 5, Name = "虚拟账户", Description = ""},
                new AccountType {Id = 6, Name = "网络账户", Description = ""},
                new AccountType {Id = 7, Name = "储值卡", Description = ""},
            };
            return result.ToArray();
        }
    }
}