using System;
using System.Data;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Service.DTO.Blog;

namespace AC.Dao.Blog
{
    public class BlogDao : DaoBase
    {
        #region Insert
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(BlogsDTO myBlogsDTO)
        {
            const string INSERT_SQL = @"
insert into MyBlogs(Title,Summary,ConverImgUrl,Content,Tags,Category,AuthorId,Author,PostTime)
values(@Title,@Summary,@ConverImgUrl,@Content,@Tags,@Category,@AuthorId,@Author,getdate())
 
select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Title", myBlogsDTO.Title);
            dbParameters.AddWithValue("Summary", myBlogsDTO.Summary);
            dbParameters.AddWithValue("ConverImgUrl", myBlogsDTO.ConverImgUrl);
            dbParameters.AddWithValue("Content", myBlogsDTO.Content);
            dbParameters.AddWithValue("Tags", myBlogsDTO.Tags);
            dbParameters.AddWithValue("Category", myBlogsDTO.Category);
            dbParameters.AddWithValue("AuthorId", myBlogsDTO.AuthorId);
            dbParameters.AddWithValue("Author", myBlogsDTO.Author);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, INSERT_SQL, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }
        #endregion

        #region GetById
        public BlogsDTO GetById(int id)
        {
            const string sqlText = @"
select  mb.Id, mb.Title, mb.Summary, mb.ConverImgUrl, mb.Content, mb.Tags, mb.Category, mb.AuthorId, mb.Author,
        mb.PostTime
from    MyBlogs mb ( nolock )
where   mb.Id = @Id";

            var dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            return DbHelper.QueryForObject(ConnStringOfAchuan, sqlText, dbParameters, new BlogsRowMapper());
        }
        #endregion

        #region  Nested type: MyBlogsRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class BlogsRowMapper : IDataTableRowMapper<BlogsDTO>
        {
            public BlogsDTO MapRow(DataRow dataReader)
            {
                var result = new BlogsDTO();
                object obj = dataReader["Id"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Id = int.Parse(obj.ToString());
                }
                result.Title = dataReader["Title"].ToString();
                result.Summary = dataReader["Summary"].ToString();
                result.ConverImgUrl = dataReader["ConverImgUrl"].ToString();
                result.Content = dataReader["Content"].ToString();
                result.Tags = dataReader["Tags"].ToString();
                result.Category = dataReader["Category"].ToString();
                obj = dataReader["AuthorId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.AuthorId = int.Parse(obj.ToString());
                }
                result.Author = dataReader["Author"].ToString();
                obj = dataReader["PostTime"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.PostTime = DateTime.Parse(obj.ToString());
                }

                return result;
            }
        }

        #endregion
    }
}