using System.Data;
using AC.Data.Core;
using AC.Service.DTO.Blog;

namespace AC.Dao.Blog
{
    public class BlogSectionsDao : DaoBase
    {
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(BlogSections blogSections)
        {
            const string INSERT_SQL = @"
insert into BlogSections(BlogId,SectionId)
values(@BlogId,@SectionId)
 
select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("BlogId", blogSections.BlogId);
            dbParameters.AddWithValue("SectionId", blogSections.SectionId);


            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, INSERT_SQL, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }

        public void DeleteByBlogId(int blogId)
        {
            const string DELETE_SQL = "delete from BlogSections where BlogId=@BlogId";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("BlogId", DbType.Int32, 4, blogId);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, DELETE_SQL, dbParameters);
        }
    }
}