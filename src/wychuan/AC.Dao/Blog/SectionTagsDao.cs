using System.Collections.Generic;
using System.Data;
using AC.Data.Core;
using AC.Service.DTO.Blog;
using AC.Util;

namespace AC.Dao.Blog
{
    public class SectionTagsDao : DaoBase
    {
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(SectionsTags sectionsTags)
        {
            const string INSERT_SQL = @"
insert into SectionsTags(SectionId,TagId)
values(@SectionId,@TagId)
 
select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("SectionId", sectionsTags.SectionId);
            dbParameters.AddWithValue("TagId", sectionsTags.TagId);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, INSERT_SQL, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }

        public void DeleteBySectionId(int sectionId)
        {
            const string sqlText = @"delete from SectionsTags where SectionId=@SectionId";
            IDbParameters dbParameters = DbHelper.CreateDbParameters("SectionId", DbType.Int32, 4, sectionId);
            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, sqlText, dbParameters);
        }

        public IList<SectionsTags> GetBySectionId(int sectionId)
        {
            const string sql = @"select st.SectionId,st.TagId,bt.TagName
from SectionsTags st(nolock)
	join BlogTags bt(nolock) on st.TagId=bt.Id
where st.SectionId=@SectionId";
            IDbParameters dbParameters = DbHelper.CreateDbParameters("SectionId", DbType.Int32, 4, sectionId);
            return DbHelper.QueryWithRowMapperDelegate(ConnStringOfAchuan, sql, dbParameters, (row =>
            {
                return new SectionsTags()
                {
                    SectionId = ParseHelper.ToInt(row["SectionId"]),
                    TagId = ParseHelper.ToInt(row["TagId"]),
                    TagName = row["TagName"].ToString()
                };
            }));
        }
    }
}