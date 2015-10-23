using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Service.DTO.LiCai;
using AC.Util;
using Common.Logging;

namespace AC.Dao.LiCai
{
    public class BillDao : DaoBase
    {
        private ILog logger = LogManager.GetLogger("WeixinLog");
        #region Insert
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(BillDTO bill)
        {
            const string insertSql = @"
insert into LC_BillDetails(UserId,BookId,Price,FirstCategoryId,FirstCategory,SecondCategoryId,SecondCategory,AccountId,ToAccountId,ConsumeTime,Remark,DetailType,BusinessId,Business,MemberId,Member,ProjectId,Project,RefundNotice,RefundTime,BaoXiao)
values(@UserId,@BookId,@Price,@FirstCategoryId,@FirstCategory,@SecondCategoryId,@SecondCategory,@AccountId,@ToAccountId,@ConsumeTime,@Remark,@DetailType,@BusinessId,@Business,@MemberId,@Member,@ProjectId,@Project,@RefundNotice,@RefundTime,@BaoXiao)

select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("UserId", bill.UserId);
            dbParameters.AddWithValue("BookId", bill.BookId);
            dbParameters.AddWithValue("Price", bill.Price);
            dbParameters.AddWithValue("FirstCategoryId", bill.FirstCategoryId);
            dbParameters.AddWithValue("FirstCategory", bill.FirstCategory);
            dbParameters.AddWithValue("SecondCategoryId", bill.SecondCategoryId);
            dbParameters.AddWithValue("SecondCategory", bill.SecondCategory);
            dbParameters.AddWithValue("AccountId", bill.AccountId);
            dbParameters.AddWithValue("ToAccountId", bill.ToAccountId);
            dbParameters.AddWithValue("ConsumeTime", bill.ConsumeTime);
            dbParameters.AddWithValue("Remark", bill.Remark);
            dbParameters.AddWithValue("DetailType", bill.DetailType);
            dbParameters.AddWithValue("BusinessId", bill.BusinessId);
            dbParameters.AddWithValue("Business", bill.Business);
            dbParameters.AddWithValue("MemberId", bill.MemberId);
            dbParameters.AddWithValue("Member", bill.Member);
            dbParameters.AddWithValue("ProjectId", bill.ProjectId);
            dbParameters.AddWithValue("Project", bill.Project);
            dbParameters.AddWithValue("RefundNotice", bill.RefundNotice);
            dbParameters.AddWithValue("RefundTime", bill.RefundTime);
            dbParameters.AddWithValue("BaoXiao", bill.BaoXiao);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, insertSql, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());

        }

        #endregion

        #region Query
        public IList<BillDTO> Query(BillQueryInfo queryInfo)
        {
            IDbParameters dbParamaters = DbHelper.CreateDbParameters();
            dbParamaters.Add("UserId", DbType.Int32, 4).Value = queryInfo.UserId;
            string condition = BindQueryCriteria(queryInfo, dbParamaters);
            string querySql = @"
select  lbd.ID, lbd.UserId, lbd.BookId, lbd.Price, lbd.FirstCategoryId, lbd.FirstCategory, lbd.SecondCategoryId,
        lbd.SecondCategory, lbd.AccountId, lbd.ToAccountId, lbd.ConsumeTime, lbd.Remark, lbd.DetailType, lbd.BusinessId,
        lbd.Business, lbd.MemberId, lbd.Member, lbd.ProjectId, lbd.Project, lbd.RefundNotice, lbd.RefundTime,
        lbd.BaoXiao
from    LC_BillDetails lbd ( nolock )
where lbd.UserId=@UserId" + condition + " order by lbd.ConsumeTime desc";
            logger.Error(querySql);
            logger.Error(JsonHelper.ToJson(queryInfo));
            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, querySql, dbParamaters, new BillRowMapper());
        }

        /// <summary>
        /// 构造查询条件
        /// </summary>
        public static string BindQueryCriteria(BillQueryInfo queryInfo, IDbParameters dbParamaters)
        {
            var stringBuilder = new StringBuilder("");
            if (queryInfo == null)
            {
                return stringBuilder.ToString();
            }

            if (queryInfo.StartTime.HasValue)
            {
                stringBuilder.AppendFormat(" and lbd.ConsumeTime>=@StartTime");
                dbParamaters.Add("StartTime", DbType.DateTime).Value = queryInfo.StartTime.Value;
            }
            if (queryInfo.EndTime.HasValue)
            {
                stringBuilder.AppendFormat(" and lbd.ConsumeTime<=@EndTime");
                dbParamaters.Add("EndTime", DbType.DateTime).Value = queryInfo.EndTime.Value;
            }
            if (queryInfo.FirstCategoryId > 0)
            {
                stringBuilder.AppendFormat(" and lbd.FirstCategoryId=@FirstCategoryId");
                dbParamaters.Add("FirstCategoryId", DbType.Int32, 4).Value = queryInfo.FirstCategoryId;
            }
            if (queryInfo.SecondCategoryId > 0)
            {
                stringBuilder.AppendFormat(" and lbd.SecondCategoryId=@SecondCategoryId");
                dbParamaters.Add("SecondCategoryId", DbType.Int32, 4).Value = queryInfo.SecondCategoryId;
            }
            if (queryInfo.AccountId > 0)
            {
                stringBuilder.AppendFormat(" and lbd.AccountId=@AccountId");
                dbParamaters.Add("AccountId", DbType.Int32, 4).Value = queryInfo.AccountId;
            }
            if (queryInfo.ProjectId > 0)
            {
                stringBuilder.AppendFormat(" and lbd.ProjectId=@ProjectId");
                dbParamaters.Add("ProjectId", DbType.Int32, 4).Value = queryInfo.ProjectId;
            }
            if (queryInfo.MemberId > 0)
            {
                stringBuilder.AppendFormat(" and lbd.MemberId=@MemberId");
                dbParamaters.Add("MemberId", DbType.Int32, 4).Value = queryInfo.MemberId;
            }
            if (queryInfo.BusinessId > 0)
            {
                stringBuilder.AppendFormat(" and lbd.BusinessId=@BusinessId");
                dbParamaters.Add("BusinessId", DbType.Int32, 4).Value = queryInfo.BusinessId;
            }
            if (queryInfo.BaoXiao.HasValue)
            {
                stringBuilder.AppendFormat(" and lbd.BaoXiao=@BaoXiao");
                dbParamaters.Add("BaoXiao", DbType.Int32, 4).Value = queryInfo.BaoXiao.Value;
            }
            return stringBuilder.ToString();
        }
        #endregion

        #region  Nested type: BillRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class BillRowMapper : IDataTableRowMapper<BillDTO>
        {
            public BillDTO MapRow(DataRow dataReader)
            {
                var result = new BillDTO();
                object obj;
                obj = dataReader["ID"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.ID = int.Parse(obj.ToString());
                }
                obj = dataReader["UserId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.UserId = int.Parse(obj.ToString());
                }
                obj = dataReader["BookId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.BookId = int.Parse(obj.ToString());
                }
                obj = dataReader["Price"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Price = decimal.Parse(obj.ToString());
                }
                obj = dataReader["FirstCategoryId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.FirstCategoryId = int.Parse(obj.ToString());
                }
                result.FirstCategory = dataReader["FirstCategory"].ToString();
                obj = dataReader["SecondCategoryId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.SecondCategoryId = int.Parse(obj.ToString());
                }
                result.SecondCategory = dataReader["SecondCategory"].ToString();
                obj = dataReader["AccountId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.AccountId = int.Parse(obj.ToString());
                }
                obj = dataReader["ToAccountId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.ToAccountId = int.Parse(obj.ToString());
                }
                obj = dataReader["ConsumeTime"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.ConsumeTime = DateTime.Parse(obj.ToString());
                }
                result.Remark = dataReader["Remark"].ToString();
                obj = dataReader["DetailType"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.DetailType = int.Parse(obj.ToString());
                }
                obj = dataReader["BusinessId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.BusinessId = int.Parse(obj.ToString());
                }
                result.Business = dataReader["Business"].ToString();
                obj = dataReader["MemberId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.MemberId = int.Parse(obj.ToString());
                }
                result.Member = dataReader["Member"].ToString();
                obj = dataReader["ProjectId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.ProjectId = int.Parse(obj.ToString());
                }
                result.Project = dataReader["Project"].ToString();
                result.RefundNotice = dataReader["RefundNotice"].ToString();
                obj = dataReader["RefundTime"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.RefundTime = DateTime.Parse(obj.ToString());
                }
                obj = dataReader["BaoXiao"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.BaoXiao = int.Parse(obj.ToString());
                }

                return result;
            }
        }

        #endregion
    }
}
