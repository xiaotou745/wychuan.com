using System;
using System.Collections.Generic;
using System.Data;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Service.DTO.Blog;

namespace AC.Dao.Blog
{
    public class BlogTagsDao : DaoBase
    {
        #region Insert

        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(BlogTagsDTO blogTagsDTO)
        {
            const string INSERT_SQL = @"
insert into BlogTags(UserId,TagName,IsEnable)
values(@UserId,@TagName,1)
 
select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("UserId", blogTagsDTO.UserId);
            dbParameters.AddWithValue("TagName", blogTagsDTO.TagName);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, INSERT_SQL, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }

        #endregion

        #region GetByUserId

        public IList<BlogTagsDTO> GetByUserId(int userId)
        {
            const string QUERY_SQL = @"
select  bt.Id, bt.UserId, bt.TagName
from    BlogTags bt ( nolock )
where   bt.IsEnable = 1
        and bt.UserId = @UserId";
            var dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);
            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, QUERY_SQL, dbParameters, new BlogTagsRowMapper());
        }

        #endregion

        #region  Nested type: BlogTagsRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class BlogTagsRowMapper : IDataTableRowMapper<BlogTagsDTO>
        {
            public BlogTagsDTO MapRow(DataRow dataReader)
            {
                var result = new BlogTagsDTO();
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
                result.TagName = dataReader["TagName"].ToString();

                return result;
            }
        }

        #endregion
    }
}