#region 引用

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

#endregion

namespace AC.Dao.Blog
{
    /// <summary>
    /// 数据访问类SectionsDao。
    /// Generate By: mywww2.wychuan.com
    /// Generate Time: 2015-12-11 17:39:47
    /// </summary>
    public class SectionsDao : DaoBase
    {
        #region Insert
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(Sections sections)
        {
            const string INSERT_SQL = @"
insert into Sections(SectionId,UserId,Title,Content,CategoryId)
values(@SectionId,@UserId, @Title,@Content,@CategoryId)
 
select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("SectionId", sections.SectionId);
            dbParameters.AddWithValue("UserId", sections.UserId);
            dbParameters.AddWithValue("Title", sections.Title);
            dbParameters.AddWithValue("Content", sections.Content);
            dbParameters.AddWithValue("CategoryId", sections.CategoryId);

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
        public void Update(Sections sections)
        {
            const string UPDATE_SQL = @"
update  Sections
set  SectionId=@SectionId,Title=@Title,Content=@Content,CategoryId=@CategoryId
where  Id=@Id ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Id", sections.Id);
            dbParameters.AddWithValue("SectionId", sections.SectionId);
            dbParameters.AddWithValue("Title", sections.Title);
            dbParameters.AddWithValue("Content", sections.Content);
            dbParameters.AddWithValue("CategoryId", sections.CategoryId);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, UPDATE_SQL, dbParameters);
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除一条记录
        /// </summary>
        public void Delete(int id)
        {
            const string DELETE_SQL = @"delete from Sections where Id=@Id ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Id", id);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, DELETE_SQL, dbParameters);
        }
        #endregion

        #region Query
        /// <summary>
        /// 查询对象
        /// </summary>
        public IList<Sections> Query(SectionsQueryInfo queryInfo)
        {
            IDbParameters dbParameters = DbHelper.CreateDbParameters();

            string condition = BindQueryCriteria(queryInfo, dbParameters);
            string querySql = @"
select  s.Id, s.SectionId, s.Title, s.Content, s.CategoryId, s.ParentId, s.CreateTime, s.UserId, s.HasChilds, s.ChildIds,
        bc.Name CategoryName, bc2.Id FirstCategoryId,
        bc2.Name FirstCategoryName
from    Sections s ( nolock )
        join BlogCategory bc ( nolock ) on bc.Id = s.CategoryId
        join BlogCategory bc2 ( nolock ) on bc.ParentId = bc2.Id where " + condition;
            return dbParameters.Count > 0
                ? DbHelper.QueryWithRowMapper(ConnStringOfAchuan, querySql, dbParameters, new SectionsRowMapper())
                : DbHelper.QueryWithRowMapper(ConnStringOfAchuan, querySql, new SectionsRowMapper());
        }
        #endregion

        #region QueryPaged
        public IPagedList<Sections> QueryPaged(SectionsQueryInfo queryInfo)
        {
            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            string where = BindQueryCriteria(queryInfo, dbParameters);

            PagedQueryBuilder<Sections> pageQuery = PagedQueryBuilder<Sections>.Create()
                .SetColumnList(@"
s.Id, s.SectionId, s.Title, s.Content, s.CategoryId, s.ParentId, s.CreateTime, s.UserId, s.HasChilds, s.ChildIds,
        bc.Name CategoryName, bc2.Id FirstCategoryId,
        bc2.Name FirstCategoryName")
                .SetOrderByColumn("s.Id")
                .SetRowMapper(new SectionsRowMapper())
                .SetTableList(@"
Sections s ( nolock )
        join BlogCategory bc ( nolock ) on bc.Id = s.CategoryId
        join BlogCategory bc2 ( nolock ) on bc.ParentId = bc2.Id")
                .SetWhere(where)
                .SetDbParameters(dbParameters)
                .SetPaginator(queryInfo);

            return QueryPaged<Sections>(pageQuery);
        }
        #endregion

        #region GetByBlogSectionIds
        public IList<Sections> GetByBlogSectionIds(string blogSectionIds)
        {
            const string QUERY_SQL = @"
select  s.Id, s.SectionId, s.Title, s.Content, s.CategoryId, 0 'ParentId', s.CreateTime, s.UserId, s.HasChilds, s.ChildIds
from    Sections s ( nolock )
where   s.SectionId in ( {0} )
union all
select  child.Id, child.SectionId, child.Title, child.Content, child.CategoryId, s.Id 'ParentId', child.CreateTime,
        child.UserId, child.HasChilds, child.ChildIds
from Sections s(nolock)
	join SectionChilds sc(nolock) on s.Id=sc.SectionId
	join Sections child(nolock) on child.Id=sc.ChildId
where s.SectionId in ( {0} )";

            string strParams;
            List<string> sectionIds = blogSectionIds.ToList();
            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddInParameters(sectionIds, DbType.String, 50, out strParams);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, QUERY_SQL.format(strParams), dbParameters,
                new SectionsRowMapper());
        }
        #endregion

        #region GetById

        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        public Sections GetById(int id)
        {
            const string GETBYID_SQL = @"
select  s.Id, s.SectionId, s.Title, s.Content, s.CategoryId, s.ParentId, s.CreateTime, s.UserId, s.HasChilds, s.ChildIds,
        bc.Name CategoryName, bc2.Id FirstCategoryId,
        bc2.Name FirstCategoryName
from    Sections s ( nolock )
        join BlogCategory bc ( nolock ) on bc.Id = s.CategoryId
        join BlogCategory bc2 ( nolock ) on bc.ParentId = bc2.Id
where   s.Id=@Id ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Id", id);

            return DbHelper.QueryForObject(ConnStringOfAchuan, GETBYID_SQL, dbParameters, new SectionsRowMapper());
        }

        #endregion

        #region Childs
        #region GetChilds

        public IList<Sections> GetChilds(int id)
        {
            const string GET_CHILDS_SQL = @"
select  s.Id, s.SectionId, s.Title, s.Content, s.CategoryId, sc.SectionId 'ParentId', s.CreateTime, s.UserId, s.HasChilds, s.ChildIds
from SectionChilds sc(nolock)
	join Sections s(nolock) on sc.ChildId=s.Id
where sc.SectionId=@SectionId";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("SectionId", DbType.Int32, 4, id);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, GET_CHILDS_SQL, dbParameters, new SectionsRowMapper());
        }

        public IList<Sections> GetChilds(IList<int> sectionIds)
        {
            const string GET_CHILDS_SQL = @"
select  s.Id, s.SectionId, s.Title, s.Content, s.CategoryId, sc.SectionId 'ParentId', s.CreateTime, s.UserId, s.HasChilds, s.ChildIds
from SectionChilds sc(nolock)
	join Sections s(nolock) on sc.ChildId=s.Id
where sc.SectionId in ({0})";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            string outSectionIds;
            dbParameters.AddInParameters(sectionIds, DbType.Int32, 4, out outSectionIds);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, GET_CHILDS_SQL.format(outSectionIds), dbParameters, new SectionsRowMapper());
        }
        #endregion
       

        #region SaveChilds

        public void SaveChild(SectionChilds child)
        {
            const string INSERT_SQL = @"
insert into SectionChilds ( SectionId, ChildId )
values  ( @SectionId,
          @ChildId 
          )";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("SectionId", child.SectionId);
            dbParameters.AddWithValue("ChildId", child.ChildId);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, INSERT_SQL, dbParameters);
        }
        #endregion

        #region DeleteChilds

        public void DeleteChilds(int id)
        {
            const string DELETE_SQL = "delete from SectionChilds where SectionId=@SectionId";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("SectionId", DbType.Int32, 4, id);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, DELETE_SQL, dbParameters);
        }
        #endregion

        #region Update Section Childs Info

        public void UpdateChildsInfo(int id, List<int> childIds)
        {
            const string UPDATE_SQL = "update Sections set HasChilds=@HasChilds,ChildIds=@ChildIds where Id=@Id";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);
            dbParameters.Add("HasChilds", DbType.Boolean).Value = (childIds != null && childIds.Count > 0);
            dbParameters.Add("ChildIds", DbType.String, 256).Value = childIds.SplitString(',');

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, UPDATE_SQL, dbParameters);
        }
        #endregion
        #endregion

        #region  Nested type: SectionsRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class SectionsRowMapper : IDataTableRowMapper<Sections>
        {
            public Sections MapRow(DataRow dataReader)
            {
                var result = new Sections();
                object obj = dataReader["Id"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Id = int.Parse(obj.ToString());
                }
                result.SectionId = dataReader["SectionId"].ToString();
                result.Title = dataReader["Title"].ToString();
                result.Content = dataReader["Content"].ToString();
                obj = dataReader["CategoryId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.CategoryId = int.Parse(obj.ToString());
                }
                obj = dataReader["ParentId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.ParentId = int.Parse(obj.ToString());
                }
                obj = dataReader["CreateTime"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.CreateTime = DateTime.Parse(obj.ToString());
                }
                result.HasChilds = ParseHelper.ToBool(dataReader["HasChilds"], false);
                result.ChildIds = dataReader["ChildIds"].ToString();

                if (dataReader.HasColumn("FirstCategoryId"))
                {
                    result.FirstCategoryId = ParseHelper.ToInt(dataReader["FirstCategoryId"], 0);
                }
                if (dataReader.HasColumn("FirstCategoryName"))
                {
                    result.FirstCategoryName = dataReader["FirstCategoryName"].ToString();
                }
                if (dataReader.HasColumn("CategoryName"))
                {
                    result.CategoryName = dataReader["CategoryName"].ToString();
                } 
                
                return result;
            }
        }

        #endregion

        #region  BindQueryCriteria

        /// <summary>
        /// 构造查询条件
        /// </summary>
        public static string BindQueryCriteria(SectionsQueryInfo queryInfo, IDbParameters dbParameters)
        {
            var stringBuilder = new StringBuilder(" 1=1 ");
            if (queryInfo == null)
            {
                return stringBuilder.ToString();
            }
            
            //TODO:在此加入查询条件构建代码
            if (queryInfo.ParentId.HasValue)
            {
                stringBuilder.Append(" and s.Id in (select sc.ChildId from SectionChilds sc(nolock) where sc.SectionId=@ParentId)");
                dbParameters.Add("ParentId", DbType.Int32, 4).Value = queryInfo.ParentId.Value;
                return stringBuilder.ToString();
            }

            if (!string.IsNullOrEmpty(queryInfo.SectionId))
            {
                stringBuilder.Append(" and s.SectionId like '%{0}%'".format(queryInfo.SectionId));
            }
            if (queryInfo.UserId > 0)
            {
                stringBuilder.Append(" and s.UserId=@UserId");
                dbParameters.Add("UserId", DbType.Int32, 4).Value = queryInfo.UserId;
            }
            
            if (queryInfo.TagIds != null && queryInfo.TagIds.Count > 0)
            {
                string strTagIds;
                dbParameters.AddInParameters(queryInfo.TagIds, DbType.Int32, 4, out strTagIds);
                stringBuilder.Append(
                    " and s.Id in (select st.SectionId from SectionsTags st(nolock) where st.TagId in ({0}))".format(
                        strTagIds));
            }
            if (queryInfo.SectionIds != null && queryInfo.SectionIds.Count > 0)
            {
                string strSectionIds;
                dbParameters.AddInParameters(queryInfo.SectionIds, DbType.String, 50, out strSectionIds);
                stringBuilder.Append(" and s.SectionId in ({0})".format(strSectionIds));
            }
            if (queryInfo.CategoryId.HasValue)
            {
                stringBuilder.Append(" and s.CategoryId=@CategoryId");
                dbParameters.Add("CategoryId", DbType.Int32, 4).Value = queryInfo.CategoryId.Value;
            }
            else if(queryInfo.FirstCategoryId.HasValue)
            {
                stringBuilder.Append(" and bc.ParentId=@FirstCategoryId");
                dbParameters.Add("FirstCategoryId", DbType.Int32, 4).Value = queryInfo.FirstCategoryId.Value;
            }
            return stringBuilder.ToString();
        }

        #endregion

        #region GetBySectionId
        public Sections GetBySectionId(string sectionId)
        {
            const string GETBYID_SQL = @"
select  s.Id, s.SectionId, s.Title, s.Content, s.CategoryId, s.ParentId, s.CreateTime, s.UserId, s.HasChilds, s.ChildIds,
        bc.Name CategoryName, bc2.Id FirstCategoryId,
        bc2.Name FirstCategoryName
from    Sections s ( nolock )
        join BlogCategory bc ( nolock ) on bc.Id = s.CategoryId
        join BlogCategory bc2 ( nolock ) on bc.ParentId = bc2.Id
where   s.SectionId=@SectionId ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("SectionId", sectionId);

            return DbHelper.QueryForObject(ConnStringOfAchuan, GETBYID_SQL, dbParameters, new SectionsRowMapper());
        }
        #endregion
    }
}