using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Extension;
using AC.Page;
using AC.Service.DTO.Blog;
using AC.Util;

namespace AC.Dao.Blog
{
    public class BlogDao : DaoBase
    {
        #region Insert

        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(BlogsDTO blog)
        {
            const string INSERT_SQL = @"
insert into MyBlogs(BlogId,Title,Summary,ConverImgUrl,CategoryId,IsPublic,SectionIds,AuthorId,Author)
values(@BlogId,@Title,@Summary,@ConverImgUrl,@CategoryId,@IsPublic,@SectionIds,@AuthorId,@Author)
 
select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("BlogId", blog.BlogId);
            dbParameters.AddWithValue("Title", blog.Title);
            dbParameters.AddWithValue("Summary", blog.Summary);
            dbParameters.AddWithValue("ConverImgUrl", blog.ConverImgUrl);
            dbParameters.AddWithValue("CategoryId", blog.CategoryId);
            dbParameters.AddWithValue("IsPublic", blog.IsPublic);
            dbParameters.AddWithValue("SectionIds", blog.SectionIds);
            dbParameters.AddWithValue("AuthorId", blog.AuthorId);
            dbParameters.AddWithValue("Author", blog.Author);

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
        public void Update(BlogsDTO blog)
        {
            const string UPDATE_SQL = @"
update  MyBlogs
set  BlogId=@BlogId,Title=@Title,Summary=@Summary,ConverImgUrl=@ConverImgUrl,CategoryId=@CategoryId,IsPublic=@IsPublic
where  Id=@Id ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Id", blog.Id);
            dbParameters.AddWithValue("BlogId", blog.BlogId);
            dbParameters.AddWithValue("Title", blog.Title);
            dbParameters.AddWithValue("Summary", blog.Summary);
            dbParameters.AddWithValue("ConverImgUrl", blog.ConverImgUrl);
            dbParameters.AddWithValue("CategoryId", blog.CategoryId);
            dbParameters.AddWithValue("IsPublic", blog.IsPublic);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, UPDATE_SQL, dbParameters);
        }

        public void UpdateSectionIds(int id, string sectionIds)
        {
            const string UPDATE_SQL = "update MyBlogs set SectionIds=@SectionIds where Id=@Id";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);
            dbParameters.Add("SectionIds", DbType.String, 50).Value = sectionIds;

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, UPDATE_SQL, dbParameters);
        }
        #endregion

        #region GetById

        public BlogsDTO GetById(int id)
        {
            const string SQL_TEXT = @"
select  s.Id, s.BlogId, s.Title, s.Summary, s.ConverImgUrl, s.CategoryId, s.IsPublic, s.SectionIds, s.AuthorId, s.Author,
        s.PostTime, bc.Name CategoryName, bc2.Id FirstCategoryId, bc2.Name FirstCategoryName
from    MyBlogs s ( nolock )
        join BlogCategory bc ( nolock ) on bc.Id = s.CategoryId
        join BlogCategory bc2 ( nolock ) on bc.ParentId = bc2.Id
where   s.Id = @Id";

            var dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            return DbHelper.QueryForObject(ConnStringOfAchuan, SQL_TEXT, dbParameters, new BlogsRowMapper());
        }

        #endregion

        #region GetByBlogId
        public BlogsDTO GetByBlogId(string bloId)
        {
            const string SQL_TEXT = @"
select  s.Id, s.BlogId, s.Title, s.Summary, s.ConverImgUrl, s.CategoryId, s.IsPublic, s.SectionIds, s.AuthorId, s.Author,
        s.PostTime, bc.Name CategoryName, bc2.Id FirstCategoryId, bc2.Name FirstCategoryName
from    MyBlogs s ( nolock )
        join BlogCategory bc ( nolock ) on bc.Id = s.CategoryId
        join BlogCategory bc2 ( nolock ) on bc.ParentId = bc2.Id
where   s.BlogId = @BlogId";

            var dbParameters = DbHelper.CreateDbParameters("BlogId", DbType.String, 50, bloId);

            return DbHelper.QueryForObject(ConnStringOfAchuan, SQL_TEXT, dbParameters, new BlogsRowMapper());
        }

        #endregion

        #region Delete
        /// <summary>
        /// 删除一条记录
        /// </summary>
        public void Delete(int id)
        {
            const string DELETE_SQL = @"delete from MyBlogs where Id=@Id ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, DELETE_SQL, dbParameters);
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
                result.BlogId = dataReader["BlogId"].ToString();
                result.Title = dataReader["Title"].ToString();
                result.Summary = dataReader["Summary"].ToString();
                result.ConverImgUrl = dataReader["ConverImgUrl"].ToString();
                obj = dataReader["CategoryId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.CategoryId = int.Parse(obj.ToString());
                }
                obj = dataReader["IsPublic"];
                if (obj != null && obj != DBNull.Value)
                {
                    if (obj.ToString() == 1.ToString() || obj.ToString().ToLower() == "true")
                    {
                        result.IsPublic = true;
                    }
                    else
                    {
                        result.IsPublic = false;
                    }
                }
                result.SectionIds = dataReader["SectionIds"].ToString();
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

                ///other
                if (dataReader.HasColumn("CategoryName"))
                {
                    result.CategoryName = dataReader["CategoryName"].ToString();
                }
                if (dataReader.HasColumn("FirstCategoryId"))
                {
                    result.FirstCategoryId = ParseHelper.ToInt(dataReader["FirstCategoryId"]);
                }
                if (dataReader.HasColumn("FirstCategoryName"))
                {
                    result.FirstCategoryName = dataReader["FirstCategoryName"].ToString();
                }
                return result;
            }
        }

        #endregion

        #region  Query Members

        public IList<BlogsDTO> Query(BlogsQueryInfo queryInfo)
        {
            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            string condition = BindQueryCriteria(queryInfo, dbParameters);
            string querySql = @"
select  s.Id, s.BlogId, s.Title, s.Summary, s.ConverImgUrl, s.CategoryId, s.IsPublic, s.SectionIds, s.AuthorId, s.Author,
        s.PostTime, bc.Name CategoryName, bc2.Id FirstCategoryId, bc2.Name FirstCategoryName
from    MyBlogs s ( nolock )
        join BlogCategory bc ( nolock ) on bc.Id = s.CategoryId
        join BlogCategory bc2 ( nolock ) on bc.ParentId = bc2.Id where " + condition;

            return dbParameters.Count == 0
                ? DbHelper.QueryWithRowMapper(ConnStringOfAchuan, querySql, new BlogsRowMapper())
                : DbHelper.QueryWithRowMapper(ConnStringOfAchuan, querySql, dbParameters, new BlogsRowMapper());
        }

        /// <summary>
        /// 构造查询条件
        /// </summary>
        public static string BindQueryCriteria(BlogsQueryInfo queryInfo, IDbParameters dbParameters)
        {
            var stringBuilder = new StringBuilder(" 1=1 ");
            if (queryInfo == null)
            {
                return stringBuilder.ToString();
            }

            //TODO:在此加入查询条件构建代码
            if (queryInfo.UserId > 0)
            {
                stringBuilder.Append(" and s.AuthorId=@UserId");
                dbParameters.Add("@UserId", DbType.Int32, 4).Value = queryInfo.UserId;
            }

            return stringBuilder.ToString();
        }

        #endregion

        public IPagedList<BlogsDTO> QueryPaged(BlogsQueryInfo queryInfo)
        {
            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            string where = BindQueryCriteria(queryInfo, dbParameters);

            PagedQueryBuilder<BlogsDTO> pageQuery = PagedQueryBuilder<BlogsDTO>.Create()
                .SetColumnList(@"
s.Id, s.BlogId, s.Title, s.Summary, s.ConverImgUrl, s.CategoryId, s.IsPublic, s.SectionIds, s.AuthorId, s.Author,
s.PostTime, bc.Name CategoryName, bc2.Id FirstCategoryId, bc2.Name FirstCategoryName")
                .SetOrderByColumn("s.Id")
                .SetRowMapper(new BlogsRowMapper())
                .SetTableList(@"
MyBlogs s ( nolock )
        join BlogCategory bc ( nolock ) on bc.Id = s.CategoryId
        join BlogCategory bc2 ( nolock ) on bc.ParentId = bc2.Id")
                .SetWhere(where)
                .SetDbParameters(dbParameters)
                .SetPaginator(queryInfo);

            return QueryPaged<BlogsDTO>(pageQuery);
        }
    }
}