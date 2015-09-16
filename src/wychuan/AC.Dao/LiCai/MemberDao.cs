using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Service.DTO.LiCai;

namespace AC.Dao.LiCai
{
    public class MemberDao : DaoBase
    {
        public void InitUserMember(int userId)
        {
            const string initSql = @"
insert  into LC_Member ( UserId, Name, IsDefault )
select  @UserId, ldv.Name, ldv.IsDefault
from    LC_DefaultValues ldv ( nolock )
where   ldv.Type = 1";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, initSql, dbParameters);
        }

        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(MemberDTO lCMemberDTO)
        {
            const string insertSql = @"
insert into LC_Member(Name,IsDefault,UserId)
values(@Name,@IsDefault,@UserId)

select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Name", lCMemberDTO.Name);
            dbParameters.AddWithValue("IsDefault", lCMemberDTO.IsDefault);
            dbParameters.AddWithValue("UserId", lCMemberDTO.UserId);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, insertSql, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }

        /// <summary>
        /// 根据UserID获取对象
        /// </summary>
        public IList<MemberDTO> GetByUserId(int userId)
        {
            const string getByUserIdSql = @"
select  Id,Name,IsDefault,UserId
from  LC_Member (nolock)
where  IsEnable=1 and UserId=@UserId ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, getByUserIdSql, dbParameters, new MemberRowMapper());
        }


        #region  Nested type: LC_MemberRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class MemberRowMapper : IDataTableRowMapper<MemberDTO>
        {
            public MemberDTO MapRow(DataRow dataReader)
            {
                var result = new MemberDTO();
                object obj;
                obj = dataReader["Id"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Id = int.Parse(obj.ToString());
                }
                result.Name = dataReader["Name"].ToString();
                obj = dataReader["IsDefault"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.IsDefault = int.Parse(obj.ToString());
                }
                obj = dataReader["UserId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.UserId = int.Parse(obj.ToString());
                }

                return result;
            }
        }

        #endregion

    }
}
