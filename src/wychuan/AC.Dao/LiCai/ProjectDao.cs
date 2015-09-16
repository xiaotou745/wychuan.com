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
    public class ProjectDao : DaoBase
    {
        public void InitUserProject(int userId)
        {
            const string initSql = @"
insert into LC_Project ( UserId, Name, Level)
select  @UserId, ldv.Name, ldv.Level
from    LC_DefaultValues ldv ( nolock )
where   ldv.Type = 2";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, initSql, dbParameters);
        }

        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(ProjectDTO lcBusinessDTO)
        {
            const string insertSql = @"
insert into lc_project(UserId,Name,Level)
values(@UserId,@Name,@Level)

select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("UserId", lcBusinessDTO.UserId);
            dbParameters.AddWithValue("Name", lcBusinessDTO.Name);
            dbParameters.AddWithValue("Level", lcBusinessDTO.Level);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, insertSql, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }

        /// <summary>
        /// 查询对象
        /// </summary>
        public IList<ProjectDTO> GetByUserId(int userId)
        {
            const string querySql = @"
select  lb.Id, lb.UserId, lb.Name, lb.Level, lb.IsEnable
from    LC_Project lb ( nolock )
where   lb.IsEnable = 1
        and lb.UserId = @UserId
order by lb.Level";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, querySql, dbParameters, new ProjectRowMapper());
        }

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class ProjectRowMapper : IDataTableRowMapper<ProjectDTO>
        {
            public ProjectDTO MapRow(DataRow dataReader)
            {
                var result = new ProjectDTO();
                object obj;
                obj = dataReader["Id"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Id = int.Parse(obj.ToString());
                }
                obj = dataReader["UserId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.UserId = int.Parse(obj.ToString());
                }
                result.Name = dataReader["Name"].ToString();
                obj = dataReader["Level"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Level = int.Parse(obj.ToString());
                }

                return result;
            }
        }

    }
}
