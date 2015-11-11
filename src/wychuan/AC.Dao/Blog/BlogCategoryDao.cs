using System;
using System.Collections.Generic;
using System.Data;
using AC.Data;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Service.DTO.Blog;

namespace AC.Dao.Blog
{
    public class BlogCategoryDao : DaoBase
    {
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(BlogCategoryDTO blogCategoryDTO)
        {
            const string INSERT_SQL = @"
insert into BlogCategory(UserId,ParentId,Name)
values(@UserId,@ParentId,@Name)
 
select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("UserId", blogCategoryDTO.UserId);
            dbParameters.AddWithValue("ParentId", blogCategoryDTO.ParentId);
            dbParameters.AddWithValue("Name", blogCategoryDTO.Name);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, INSERT_SQL, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }

        public IList<BlogCategoryDTO> GetByUserId(int userId)
        {
            const string QUERY_SQL = @"
select  bc.Id, bc.UserId, bc.ParentId, bc.Name
from    BlogCategory bc ( nolock )
where   bc.IsEnable = 1
        and bc.UserId = @UserId";
            var dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);
            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, QUERY_SQL, dbParameters, new BlogCategoryRowMapper());
        }

        #region  Nested type: BlogCategoryRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class BlogCategoryRowMapper : IDataTableRowMapper<BlogCategoryDTO>
        {
            public BlogCategoryDTO MapRow(DataRow dataReader)
            {
                var result = new BlogCategoryDTO();
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
                obj = dataReader["ParentId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.ParentId = int.Parse(obj.ToString());
                }
                result.Name = dataReader["Name"].ToString();
                
                return result;
            }
        }

        #endregion

        public void Rename(int id, string name)
        {
            const string SQL_TEXT = @"update BlogCategory set Name=@Name where Id=@Id";

            var dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);
            dbParameters.Add("Name", DbType.String, 50).Value = name;

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, SQL_TEXT, dbParameters);
        }

        public void Delete(int id)
        {
            const string SQL_TEXT = @"update BlogCategory set IsEnable=0 where Id=@Id";

            var dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, SQL_TEXT, dbParameters);
        }
    }
}