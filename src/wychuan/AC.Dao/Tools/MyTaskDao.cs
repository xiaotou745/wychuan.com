using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Extension;
using AC.Service.DTO.Tools;

namespace AC.Dao.Tools
{
    public class MyTaskDao : DaoBase
    {
        #region Insert
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(MyTaskDTO myTasksDTO)
        {
            const string insertSql = @"
insert into MyTasks(UserId,Title,Content)
values(@UserId,@Title,@Content)

select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("UserId", myTasksDTO.UserId);
            dbParameters.AddWithValue("Title", myTasksDTO.Title);
            dbParameters.AddWithValue("Content", myTasksDTO.Content);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, insertSql, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }
        #endregion

        #region GetMyTasks
        public IList<MyTaskDTO> GetMyTask(MyTaskQueryInfo queryInfo)
        {
            const string getSql = @"
select  mt.Id, mt.UserId, mt.Status, mt.PubTime, mt.Title, mt.Content, mt.IsEnable, 
        mt.Class, mt.Remark
from    MyTasks mt ( nolock )
where   mt.IsEnable = 1 and mt.IsShow=1 {0}";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();

            string queryStr = BindQueryCriteria(queryInfo, dbParameters);

            return dbParameters.Count > 0
                ? DbHelper.QueryWithRowMapper(ConnStringOfAchuan, getSql.format(queryStr), dbParameters,
                    new MyTasksRowMapper())
                : DbHelper.QueryWithRowMapper(ConnStringOfAchuan, getSql.format(queryStr), new MyTasksRowMapper());
        }
        #endregion
        
        #region ChangeStatus

        public void UpdateStatus(int id, int targetStatus)
        {
            const string strSql = @"update MyTasks set Status=@Status where Id=@Id";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.Add("Id", DbType.Int32, 4).Value = id;
            dbParameters.Add("Status", DbType.Int32, 4).Value = targetStatus;

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, strSql, dbParameters);
        }
        #endregion

        #region  Nested type: MyTasksRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class MyTasksRowMapper : IDataTableRowMapper<MyTaskDTO>
        {
            public MyTaskDTO MapRow(DataRow dataReader)
            {
                var result = new MyTaskDTO();
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
                obj = dataReader["Status"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Status = int.Parse(obj.ToString());
                }
                obj = dataReader["PubTime"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.PubTime = DateTime.Parse(obj.ToString());
                }
                result.Title = dataReader["Title"].ToString();
                result.Content = dataReader["Content"].ToString();
                result.Class = dataReader["Class"].ToString();
                result.Remark = dataReader["Remark"].ToString();
                
                return result;
            }
        }

        #endregion

        #region  Other Members

        /// <summary>
        /// 构造查询条件
        /// </summary>
        public static string BindQueryCriteria(MyTaskQueryInfo queryInfo, IDbParameters dbParameters)
        {
            var stringBuilder = new StringBuilder(string.Empty);
            if (queryInfo == null)
            {
                return stringBuilder.ToString();
            }

            //TODO:在此加入查询条件构建代码
            if (queryInfo.UserId > 0)
            {
                stringBuilder.Append(" and mt.UserId=@UserId");
                dbParameters.Add("UserId", DbType.Int32, 4).Value = queryInfo.UserId;
            }

            if (queryInfo.StartDate.HasValue)
            {
                stringBuilder.Append(" and mt.PubTime>=@StartTime");
                dbParameters.Add("StartTime", DbType.DateTime).Value = queryInfo.StartDate.Value;
            }

            if (queryInfo.EndDate.HasValue)
            {
                stringBuilder.Append(" and mt.PubTime<=@EndTime");
                dbParameters.Add("EndTime", DbType.DateTime).Value = queryInfo.EndDate.Value;
            }

            return stringBuilder.ToString();
        }

        #endregion

        #region Update
        public void Update(MyTaskDTO myTasksDTO)
        {
            const string updateSql = @"
update  MyTasks
set  Title=@Title,Content=@Content
where  Id=@Id ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Id", myTasksDTO.Id);
            dbParameters.AddWithValue("Title", myTasksDTO.Title);
            dbParameters.AddWithValue("Content", myTasksDTO.Content);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, updateSql, dbParameters);
        }
        #endregion

        #region Hide or Show

        public void Hide(int id)
        {
            const string strSql = "update MyTasks set IsShow=0 where Id=@Id";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, strSql, dbParameters);
        }
        #endregion

        #region Delete
        public void Delete(int id)
        {
            const string strSql = "update MyTasks set IsEnable=0 where Id=@Id";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, strSql, dbParameters);
        }
        #endregion
    }
}
