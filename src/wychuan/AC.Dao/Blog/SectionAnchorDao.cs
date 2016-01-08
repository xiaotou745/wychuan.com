using System;
using System.Collections.Generic;
using System.Data;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Extension;
using AC.Service.DTO.Blog;

namespace AC.Dao.Blog
{
    public class SectionAnchorDao : DaoBase
    {
        #region Insert

        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(SectionAnchors sectionAnchors)
        {
            const string INSERT_SQL = @"
insert into SectionAnchors(SectionId,AnchorId,AnchorText)
values(@SectionId,@AnchorId,@AnchorText)
 
select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("SectionId", sectionAnchors.SectionId);
            dbParameters.AddWithValue("AnchorId", sectionAnchors.AnchorId);
            dbParameters.AddWithValue("AnchorText", sectionAnchors.AnchorText);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, INSERT_SQL, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }

        #endregion

        #region Update
        /// <summary>
        /// 更新一条记录
        /// </summary>
        public void Update(SectionAnchors sectionAnchors)
        {
            const string UPDATE_SQL = @"
update  SectionAnchors
set  AnchorId=@AnchorId,AnchorText=@AnchorText
where  Id=@Id ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Id", sectionAnchors.Id);
            dbParameters.AddWithValue("AnchorId", sectionAnchors.AnchorId);
            dbParameters.AddWithValue("AnchorText", sectionAnchors.AnchorText);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, UPDATE_SQL, dbParameters);
        }
        #endregion

        #region Delete

        public void Delete(int id)
        {
            const string DELETE_SQL = "delete from SectionAnchors where Id=@Id";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, DELETE_SQL, dbParameters);
        }
        #endregion

        #region DeleteBySectionId

        public void DeleteBySectionId(int sectionId)
        {
            const string DELETE_SQL = "delete from SectionAnchors where SectionId=@SectionId";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("SectionId", DbType.Int32, 4, sectionId);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, DELETE_SQL, dbParameters);
        }

        #endregion

        #region GetBySectionId

        public IList<SectionAnchors> GetBySectionId(int sectionId)
        {
            const string SELETE_SQL = @"
select  sa.Id, sa.SectionId, sa.AnchorId, sa.AnchorText
from    SectionAnchors sa ( nolock )
where   sa.SectionId = @SectionId";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("SectionId", DbType.Int32, 4, sectionId);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, SELETE_SQL, dbParameters,
                new SectionAnchorsRowMapper());
        }

        #endregion

        #region GetBySectionIds

        public IList<SectionAnchors> GetBySectionIds(IList<int> sectionIds)
        {
            const string GET_SQL = @"
select  sa.Id, sa.SectionId, sa.AnchorId, sa.AnchorText
from    SectionAnchors sa ( nolock )
where   sa.SectionId in ( {0} )";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            string strParams;
            dbParameters.AddInParameters(sectionIds, DbType.Int32, 4, out strParams);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, GET_SQL.format(strParams), dbParameters,
                new SectionAnchorsRowMapper());
        }
        #endregion

        #region  Nested type: SectionAnchorsRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class SectionAnchorsRowMapper : IDataTableRowMapper<SectionAnchors>
        {
            public SectionAnchors MapRow(DataRow dataReader)
            {
                var result = new SectionAnchors();
                object obj = dataReader["Id"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Id = int.Parse(obj.ToString());
                }
                obj = dataReader["SectionId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.SectionId = int.Parse(obj.ToString());
                }
                result.AnchorId = dataReader["AnchorId"].ToString();
                result.AnchorText = dataReader["AnchorText"].ToString();

                return result;
            }
        }

        #endregion
    }
}