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
    public class BusinessDao : DaoBase
    {
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(BusinessDTO lcBusinessDTO)
        {
            const string insertSql = @"
insert into lc_business(UserId,Name,Level)
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
        public IList<BusinessDTO> GetByUserId(int userId)
        {
            const string querySql = @"
select  lb.Id, lb.UserId, lb.Name, lb.Level, lb.IsEnable
from    LC_Business lb ( nolock )
where   lb.IsEnable = 1
        and lb.UserId = @UserId
order by lb.Level";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, querySql, dbParameters, new BusinessRowMapper());
        }

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class BusinessRowMapper : IDataTableRowMapper<BusinessDTO>
        {
            public BusinessDTO MapRow(DataRow dataReader)
            {
                var result = new BusinessDTO();
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
