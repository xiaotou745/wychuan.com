using System.Collections.Generic;
using System.Data;
using AC.Data.Core;
using AC.Helper;
using AC.Service.DTO.Blog;

namespace AC.Dao.Blog
{
    public class BlogTagRelationDao : DaoBase
    {
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(BlogTagRelation blogTagReletion)
        {
            const string INSERT_SQL = @"
insert into BlogTagReletion(BlogId,TagId)
values(@BlogId,@TagId)
 
select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("BlogId", blogTagReletion.BlogId);
            dbParameters.AddWithValue("TagId", blogTagReletion.TagId);


            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, INSERT_SQL, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }

        public void DeleteByBlogId(int blogId)
        {
            const string SQL_TEXT = @"delete from BlogTagReletion where BlogId=@BlogId";
            IDbParameters dbParameters = DbHelper.CreateDbParameters("BlogId", DbType.Int32, 4, blogId);
            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, SQL_TEXT, dbParameters);
        }

        public IList<BlogTagRelation> GetByBlogId(int blogId)
        {
            const string SQL_TEXT = @"
select btr.BlogId,btr.TagId,bt.TagName
from BlogTagReletion btr(nolock)
	join BlogTags bt(nolock) on btr.TagId=bt.Id
where btr.BlogId=@BlogId";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("BlogId", DbType.Int32, 4, blogId);

            return DbHelper.QueryWithRowMapperDelegate(ConnStringOfAchuan, SQL_TEXT, dbParameters, (dataRow =>
            {
                return new BlogTagRelation
                {
                    BlogId = ParseHelper.ToInt(dataRow["BlogId"]),
                    TagId = ParseHelper.ToInt(dataRow["TagId"]),
                    TagName = dataRow["TagName"].ToString()
                };
            }));
        }
    }
}