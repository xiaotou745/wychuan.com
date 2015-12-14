﻿#region 引用

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AC.Data.Core;
using AC.Data.Generic;
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
        #region ISectionsDao  Members

        #region Insert
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(Sections sections)
        {
            const string INSERT_SQL = @"
insert into Sections(SectionId,UserId,Title,Content,CategoryId,ParentId)
values(@SectionId,@UserId, @Title,@Content,@CategoryId,@ParentId)
 
select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("SectionId", sections.SectionId);
            dbParameters.AddWithValue("UserId", sections.UserId);
            dbParameters.AddWithValue("Title", sections.Title);
            dbParameters.AddWithValue("Content", sections.Content);
            dbParameters.AddWithValue("CategoryId", sections.CategoryId);
            dbParameters.AddWithValue("ParentId", sections.ParentId);

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
set  SectionId=@SectionId,Title=@Title,Content=@Content,CategoryId=@CategoryId,ParentId=@ParentId
where  Id=@Id ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Id", sections.Id);
            dbParameters.AddWithValue("SectionId", sections.SectionId);
            dbParameters.AddWithValue("Title", sections.Title);
            dbParameters.AddWithValue("Content", sections.Content);
            dbParameters.AddWithValue("CategoryId", sections.CategoryId);
            dbParameters.AddWithValue("ParentId", sections.ParentId);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, UPDATE_SQL, dbParameters);
        }
        #endregion
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

        /// <summary>
        /// 查询对象
        /// </summary>
        public IList<Sections> Query(SectionsQueryInfo queryInfo)
        {
            IDbParameters dbParameters = DbHelper.CreateDbParameters();

            string condition = BindQueryCriteria(queryInfo, dbParameters);
            string querySql = @"
select  s.Id, s.SectionId, s.Title, s.Content, s.CategoryId, s.ParentId, s.CreateTime, s.UserId, 
        bc.Name CategoryName, bc2.Id FirstCategoryId,
        bc2.Name FirstCategoryName
from    Sections s ( nolock )
        join BlogCategory bc ( nolock ) on bc.Id = s.CategoryId
        join BlogCategory bc2 ( nolock ) on bc.ParentId = bc2.Id" + condition;
            return dbParameters.Count > 0
                ? DbHelper.QueryWithRowMapper(ConnStringOfAchuan, querySql, dbParameters, new SectionsRowMapper())
                : DbHelper.QueryWithRowMapper(ConnStringOfAchuan, querySql, new SectionsRowMapper());
        }


        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        public Sections GetById(int id)
        {
            const string GETBYID_SQL = @"
select  s.Id, s.SectionId, s.Title, s.Content, s.CategoryId, s.ParentId, s.CreateTime, s.UserId, 
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
                result.CategoryName = dataReader["CategoryName"].ToString();
                result.FirstCategoryId = ParseHelper.ToInt(dataReader["FirstCategoryId"], 0);
                result.FirstCategoryName = dataReader["FirstCategoryName"].ToString();
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

                return result;
            }
        }

        #endregion

        #region  Other Members

        /// <summary>
        /// 构造查询条件
        /// </summary>
        public static string BindQueryCriteria(SectionsQueryInfo queryInfo, IDbParameters dbParameters)
        {
            var stringBuilder = new StringBuilder(" where 1=1 ");
            if (queryInfo == null)
            {
                return stringBuilder.ToString();
            }
            
            //TODO:在此加入查询条件构建代码
            if (queryInfo.UserId > 0)
            {
                stringBuilder.Append(" and s.UserId=@UserId");
                dbParameters.Add("UserId", DbType.Int32, 4).Value = queryInfo.UserId;
            }
            if (!string.IsNullOrEmpty(queryInfo.SectionId))
            {
                stringBuilder.Append(" and s.SectionId=@SectionId");
                dbParameters.Add("SectionId", DbType.String, 50).Value = queryInfo.SectionId;
            }
            if (queryInfo.ParentId > 0)
            {
                stringBuilder.Append(" and s.ParentId=@ParentId");
                dbParameters.Add("ParentId", DbType.Int32, 4).Value = queryInfo.ParentId;
            }
            return stringBuilder.ToString();
        }

        #endregion

        public Sections GetBySectionId(string sectionId)
        {
            const string GETBYID_SQL = @"
select  s.Id, s.SectionId, s.Title, s.Content, s.CategoryId, s.ParentId, s.CreateTime, s.UserId, 
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
    }
}