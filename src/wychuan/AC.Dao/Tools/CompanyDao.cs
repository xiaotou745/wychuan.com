using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Service.DTO.Tools;

namespace AC.Dao.Tools
{
    public class CompanyDao : DaoBase
    {
        #region Insert

        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(CompanyDTO companyDTO)
        {
            const string insertSql = @"
insert into Company(UserId,Name,Introdution,Creator,CreateTime,Remark)
values(@UserId,@Name,@Introdution,@Creator,@CreateTime,@Remark)

select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("UserId", companyDTO.UserId);
            dbParameters.AddWithValue("Name", companyDTO.Name);
            dbParameters.AddWithValue("Introdution", companyDTO.Introdution);
            dbParameters.AddWithValue("Creator", companyDTO.Creator);
            dbParameters.AddWithValue("CreateTime", companyDTO.CreateTime);
            dbParameters.AddWithValue("Remark", companyDTO.Remark);


            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, insertSql, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }
        #endregion

        #region GetByUserId

        public IList<CompanyDTO> GetByUserId(int userId)
        {
            const string getbyidSql = @"
select  c.Id, c.UserId, c.Name, c.Introdution, c.Creator, c.CreateTime, c.Remark
from    Company c ( nolock )
where   c.IsEnable = 1
        and c.UserId = @UserId";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, getbyidSql, dbParameters, new CompanyRowMapper());

        }
        #endregion

        #region  Nested type: CompanyRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class CompanyRowMapper : IDataTableRowMapper<CompanyDTO>
        {
            public CompanyDTO MapRow(DataRow dataReader)
            {
                var result = new CompanyDTO();
                object obj = dataReader["Id"];
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
                result.Introdution = dataReader["Introdution"].ToString();
                result.Creator = dataReader["Creator"].ToString();
                obj = dataReader["CreateTime"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.CreateTime = DateTime.Parse(obj.ToString());
                }
                result.Remark = dataReader["Remark"].ToString();

                return result;
            }
        }

        #endregion

    }
}
