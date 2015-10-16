using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Service.DTO.Tools;

namespace AC.Dao.Tools
{
    public class MyNotesDao: DaoBase
    {
        #region Insert

        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(MyNotesDTO myNotesDTO)
        {
            const string insertSql = @"
insert into MyNotes(UserId,PubTime,Title,Content,IsEnable)
values(@UserId,getdate(),@Title,@Content,1)
 
select @@IDENTITY";
 
            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("UserId", myNotesDTO.UserId);
            dbParameters.AddWithValue("Title", myNotesDTO.Title);
            dbParameters.AddWithValue("Content", myNotesDTO.Content);
 
            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, insertSql, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }
        #endregion

        #region Query

        public IList<MyNotesDTO> Query(MyNotesQueryInfo queryInfo)
        {
            IDbParameters dbParameters = DbHelper.CreateDbParameters();

            string condition = BindQueryCriteria(queryInfo, dbParameters);
            string querySql = @"
select  mn.Id, mn.UserId, mn.PubTime, mn.Title, mn.Content
from    MyNotes mn ( nolock )
where   mn.IsEnable=1" + condition;
            return dbParameters.Count == 0
                ? DbHelper.QueryWithRowMapper(ConnStringOfAchuan, querySql, new MyNotesRowMapper())
                : DbHelper.QueryWithRowMapper(ConnStringOfAchuan, querySql, dbParameters, new MyNotesRowMapper());
        }

        /// <summary>
        /// 构造查询条件
        /// </summary>
        public static string BindQueryCriteria(MyNotesQueryInfo queryInfo, IDbParameters dbParameters)
        {
            var stringBuilder = new StringBuilder("");
            if (queryInfo == null)
            {
                return stringBuilder.ToString();
            }

            //TODO:在此加入查询条件构建代码
            if (queryInfo.UserId.HasValue)
            {
                stringBuilder.AppendFormat(" and mn.UserId=@UserId");
                dbParameters.Add("UserId", DbType.Int32, 4).Value = queryInfo.UserId;
            }
            if (queryInfo.CreateTimeRange.StartTime.HasValue)
            {
                stringBuilder.AppendFormat(" and mn.PubTime>=@StartDate");
                dbParameters.Add("StartDate", DbType.DateTime).Value = queryInfo.CreateTimeRange.StartTime;
            }
            if (queryInfo.CreateTimeRange.OverTime.HasValue)
            {
                stringBuilder.AppendFormat(" and mn.PubTime<=@OverDate");
                dbParameters.Add("OverDate", DbType.DateTime).Value = queryInfo.CreateTimeRange.OverTime;
            }

            return stringBuilder.ToString();
        }
        #endregion

        #region Delete

        public void Delete(int id)
        {
            const string deleteSql = @"update MyNotes set IsEnable=0 where Id=@Id";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, deleteSql, dbParameters);
        }
        #endregion

        #region  Nested type: MyNotesRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class MyNotesRowMapper : IDataTableRowMapper<MyNotesDTO>
        {
            public MyNotesDTO MapRow(DataRow dataReader)
            {
                var result = new MyNotesDTO();
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
                obj = dataReader["PubTime"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.PubTime = DateTime.Parse(obj.ToString());
                }
                result.Title = dataReader["Title"].ToString();
                result.Content = dataReader["Content"].ToString();

                return result;
            }
        }

        #endregion
    }
}
