﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Extension;
using AC.Helper;
using AC.Service.DTO.LiCai;

namespace AC.Dao.LiCai
{
    public class AccountDao: DaoBase
    {
        #region Insert
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(AccountDTO lcAccountInfo)
        {
            const string insertSql = @"
insert into lc_account(UserId,Name,Type,Balance,Currency,InNetAssets,Remark,CreateBy,CreateTime,StatementDate,RepaymentDate,AccountLimit)
values(@UserId,@Name,@Type,@Balance,@Currency,@InNetAssets,@Remark,@CreateBy,getdate(),@StatementDate,@RepaymentDate,@AccountLimit)

select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("UserId", lcAccountInfo.UserId);
            dbParameters.AddWithValue("Name", lcAccountInfo.Name);
            dbParameters.AddWithValue("Type", lcAccountInfo.Type);
            dbParameters.AddWithValue("Balance", lcAccountInfo.Balance);
            dbParameters.AddWithValue("Currency", lcAccountInfo.Currency);
            dbParameters.AddWithValue("InNetAssets", lcAccountInfo.InNetAssets);
            dbParameters.AddWithValue("Remark", lcAccountInfo.Remark);
            dbParameters.AddWithValue("CreateBy", lcAccountInfo.CreateBy);
            dbParameters.AddWithValue("StatementDate", lcAccountInfo.StatementDate);
            dbParameters.AddWithValue("RepaymentDate", lcAccountInfo.RepaymentDate);
            dbParameters.AddWithValue("AccountLimit", lcAccountInfo.AccountLimit);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, insertSql, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }
        #endregion

        #region Query
        /// <summary>
        /// 查询对象
        /// </summary>
        public IList<AccountDTO> Query(AccountQueryInfo queryInfo)
        {
            IDbParameters dbParameters = DbHelper.CreateDbParameters();

            string condition = BindQueryCriteria(queryInfo, dbParameters);
            string querySql = @"
select  la.Id, la.UserId, la.Name, la.Type, la.Balance, la.Currency, la.InNetAssets, la.Remark, la.CreateBy,
        la.CreateTime,la.StatementDate,la.RepaymentDate,la.IsEnable,la.AccountLimit
from    LC_Account la ( nolock )" + condition;
            return dbParameters.Count > 0
                ? DbHelper.QueryWithRowMapper(ConnStringOfAchuan, querySql, dbParameters, new AccountRowMapper())
                : DbHelper.QueryWithRowMapper(ConnStringOfAchuan, querySql, new AccountRowMapper());
        }
        #endregion

        #region Delete

        public void Delete(int id)
        {
            const string deleteSql = @"update LC_Account set IsEnable=0 where Id=@Id";
            IDbParameters dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);
            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, deleteSql, dbParameters);
        }
        #endregion

        #region Update

        public void Update(AccountDTO account)
        {
            const string UPDATE_SQL = @"
update  LC_Account
set     Name = @Name, Type = @Type, Balance = @Balance, Currency = @Currency,
        InNetAssets = @InNetAssets, Remark = @Remark,StatementDate=@StatementDate,
        RepaymentDate=@RepaymentDate,AccountLimit=@AccountLimit
where   Id = @Id ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Id", account.Id);
            dbParameters.AddWithValue("Name", account.Name);
            dbParameters.AddWithValue("Type", account.Type);
            dbParameters.AddWithValue("Balance", account.Balance);
            dbParameters.AddWithValue("Currency", account.Currency);
            dbParameters.AddWithValue("InNetAssets", account.InNetAssets);
            dbParameters.AddWithValue("Remark", account.Remark);
            dbParameters.AddWithValue("StatementDate", account.StatementDate);
            dbParameters.AddWithValue("RepaymentDate", account.RepaymentDate);
            dbParameters.AddWithValue("AccountLimit", account.AccountLimit);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, UPDATE_SQL, dbParameters);
        }
        #endregion

        public void AdjustBalance(int id, decimal adjustPrice)
        {
            const string SQL_TEXT = @"update LC_Account set Balance=Balance+@AdjustPrice where Id=@Id";

            var dbParameters = DbHelper.CreateDbParameters();

            dbParameters.Add("Id", DbType.Int32, 4).Value = id;
            dbParameters.Add("AdjustPrice", DbType.Decimal).Value = adjustPrice;

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, SQL_TEXT, dbParameters);
        }


        #region GetById

        public AccountDTO GetById(int id)
        {
            IDbParameters dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            const string GET_BY_ID_SQL = @"
select  la.Id, la.UserId, la.Name, la.Type, la.Balance, la.Currency, la.InNetAssets, la.Remark, la.CreateBy,
        la.CreateTime,la.StatementDate,la.RepaymentDate,la.AccountLimit,la.IsEnable
from    LC_Account la ( nolock )
where   la.Id=@Id";

            return DbHelper.QueryForObject(ConnStringOfAchuan, GET_BY_ID_SQL, dbParameters, new AccountRowMapper());
        }
        #endregion

        #region  Other Members

        /// <summary>
        /// 构造查询条件
        /// </summary>
        public static string BindQueryCriteria(AccountQueryInfo queryInfo, IDbParameters dbParameters)
        {
            var stringBuilder = new StringBuilder(" where 1=1");
            if (queryInfo == null)
            {
                return stringBuilder.ToString();
            }
            if (queryInfo.IsEnable.HasValue)
            {
                stringBuilder.AppendFormat(" and la.IsEnable=@IsEnable");
                dbParameters.Add("IsEnable", DbType.Boolean).Value = queryInfo.IsEnable.Value;
            }
            //TODO:在此加入查询条件构建代码
            if (queryInfo.UserId.HasValue)
            {
                stringBuilder.AppendFormat(" and la.UserId=@UserId");
                dbParameters.Add("UserId", DbType.Int32, 4).Value = queryInfo.UserId.Value;
            }
            if (queryInfo.AccountTypeId.HasValue)
            {
                stringBuilder.AppendFormat(" and la.Type=@Type");
                dbParameters.Add("Type", DbType.Int32, 4).Value = queryInfo.AccountTypeId.Value;
            }
            return stringBuilder.ToString();
        }

        #endregion


        #region  Nested type: lc_accountRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class AccountRowMapper : IDataTableRowMapper<AccountDTO>
        {
            public AccountDTO MapRow(DataRow dataReader)
            {
                var result = new AccountDTO();
                object obj = dataReader["Id"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Id = int.Parse(obj.ToString());
                }
                obj = dataReader["UserId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.UserId = int.Parse(obj.ToString());
                }
                result.Name = dataReader["Name"].ToString();
                obj = dataReader["Type"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Type = int.Parse(obj.ToString());
                }
                obj = dataReader["Balance"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Balance = decimal.Parse(obj.ToString());
                }
                result.Currency = dataReader["Currency"].ToString();
                obj = dataReader["InNetAssets"];
                if (obj != null && obj != DBNull.Value)
                {
                    if (obj.ToString() == 1.ToString() || obj.ToString().ToLower() == "true")
                    {
                        result.InNetAssets = true;
                    }
                    else
                    {
                        result.InNetAssets = false;
                    }
                }
                obj = dataReader["IsEnable"];
                if (obj != null && obj != DBNull.Value)
                {
                    if (obj.ToString() == 1.ToString() || obj.ToString().ToLower() == "true")
                    {
                        result.IsEnable = true;
                    }
                    else
                    {
                        result.IsEnable = false;
                    }
                }
                result.Remark = dataReader["Remark"].ToString();
                result.CreateBy = dataReader["CreateBy"].ToString();
                obj = dataReader["CreateTime"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.CreateTime = DateTime.Parse(obj.ToString());
                }
                obj = dataReader["StatementDate"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.StatementDate = ParseHelper.ToInt(obj);
                }
                obj = dataReader["RepaymentDate"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.RepaymentDate = ParseHelper.ToInt(obj);
                }
                result.AccountLimit = ParseHelper.ToDecimal(dataReader["AccountLimit"]);
                return result;
            }
        }

        #endregion

        public Dictionary<int, int> GetTypeCounts(int userId)
        {
            const string querySql = @"
select la.Type,count(1) 'Count'
from LC_Account la(nolock)
where la.IsEnable=1
	and la.UserId=@UserId
group by la.Type";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            var result = new Dictionary<int, int>();
            DataSet dataSet = DbHelper.ExecuteDataset(ConnStringOfAchuan, querySql, dbParameters);
            if (!dataSet.IsEmpty())
            {
                foreach (DataRow datarow in dataSet.Tables[0].Rows)
                {
                    result.Add(ParseHelper.ToInt(datarow["Type"]), ParseHelper.ToInt(datarow["Count"]));
                }
            }
            return result;
        }
    }
}
