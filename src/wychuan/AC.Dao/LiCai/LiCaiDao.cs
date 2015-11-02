using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Service.DTO.LiCai;

namespace AC.Dao.LiCai
{
    public class LiCaiDao:DaoBase
    {
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(LiCaiDetailsDTO lCLiCaiDetailsDTO)
        {
            const string INSERT_SQL = @"
insert into LC_LiCaiDetails(UserId,AccountId,Project,BuyDay,Times,TimeUnit,Price,InterestRate,ExpireDay,RedeemDay)
values(@UserId,@AccountId,@Project,@BuyDay,@Times,@TimeUnit,@Price,@InterestRate,@ExpireDay,@RedeemDay)
 
select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("UserId", lCLiCaiDetailsDTO.UserId);
            dbParameters.AddWithValue("AccountId", lCLiCaiDetailsDTO.AccountId);
            dbParameters.AddWithValue("Project", lCLiCaiDetailsDTO.Project);
            dbParameters.AddWithValue("BuyDay", lCLiCaiDetailsDTO.BuyDay);
            dbParameters.AddWithValue("Times", lCLiCaiDetailsDTO.Times);
            dbParameters.AddWithValue("TimeUnit", lCLiCaiDetailsDTO.TimeUnit);
            dbParameters.AddWithValue("Price", lCLiCaiDetailsDTO.Price);
            dbParameters.AddWithValue("InterestRate", lCLiCaiDetailsDTO.InterestRate);
            dbParameters.AddWithValue("ExpireDay", lCLiCaiDetailsDTO.ExpireDay);
            dbParameters.AddWithValue("RedeemDay", lCLiCaiDetailsDTO.RedeemDay);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, INSERT_SQL, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }

        public IList<LiCaiDetailsDTO> GetByUserId(int userId)
        {
            const string GET_SQL = @"
select  llcd.Id, llcd.UserId, llcd.AccountId, llcd.Project, llcd.BuyDay, llcd.Times, llcd.TimeUnit, llcd.Price,
        llcd.InterestRate, llcd.ExpireDay, llcd.RedeemDay, llcd.RedeemPrice, la.Name
from LC_LiCaiDetails llcd(nolock)
    join LC_Account la(nolock) on llcd.AccountId=la.Id
where llcd.IsEnable=1 and llcd.UserId=@UserId";
            var dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, GET_SQL, dbParameters, new LiCaiDetailsRowMapper());
        }

        #region  Nested type: LC_LiCaiDetailsRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class LiCaiDetailsRowMapper : IDataTableRowMapper<LiCaiDetailsDTO>
        {
            public LiCaiDetailsDTO MapRow(DataRow dataReader)
            {
                var result = new LiCaiDetailsDTO();
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
                obj = dataReader["AccountId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.AccountId = int.Parse(obj.ToString());
                }
                result.Project = dataReader["Project"].ToString();
                obj = dataReader["BuyDay"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.BuyDay = DateTime.Parse(obj.ToString());
                }
                obj = dataReader["Times"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Times = int.Parse(obj.ToString());
                }
                result.TimeUnit = dataReader["TimeUnit"].ToString();
                obj = dataReader["Price"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Price = decimal.Parse(obj.ToString());
                }
                obj = dataReader["InterestRate"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.InterestRate = decimal.Parse(obj.ToString());
                }
                obj = dataReader["ExpireDay"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.ExpireDay = DateTime.Parse(obj.ToString());
                }
                obj = dataReader["RedeemDay"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.RedeemDay = DateTime.Parse(obj.ToString());
                }
                obj = dataReader["RedeemPrice"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.RedeemPrice = decimal.Parse(obj.ToString());
                }
                result.Account = dataReader["Name"].ToString();
                return result;
            }
        }
        #endregion
    }
}
