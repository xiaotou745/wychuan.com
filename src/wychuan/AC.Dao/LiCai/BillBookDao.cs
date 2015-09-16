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
    public class BillBookDao : DaoBase
    {
        public void InitUserBooks(int userId)
        {
            const string initSql = @"
insert  into LC_BillBooks ( Name, UserId, CreateBy, IsDefault )
select  ldv.Name, @UserId, 'system', ldv.IsDefault
from    LC_DefaultValues ldv ( nolock )
where   ldv.Type = 3";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, initSql, dbParameters);
        }
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(BillBooksDTO book)
        {
            const string insertSql = @"
insert into LC_BillBooks(Name,UserId,CreateBy)
values(@Name,@UserId,@CreateBy)

select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Name", book.Name);
            dbParameters.AddWithValue("UserId", book.UserId);
            dbParameters.AddWithValue("CreateBy", book.CreateBy);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, insertSql, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        public void Update(int id, string name)
        {
            const string updateSql = @"
update  LC_BillBooks
set  Name=@Name
where  ID=@ID ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("ID", id);
            dbParameters.AddWithValue("Name", name);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, updateSql, dbParameters);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        public void Delete(int id)
        {
            const string deleteSql = @"update LC_BillBooks set IsEnable=0 where ID=@Id";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, deleteSql, dbParameters);
        }

        public IList<BillBooksDTO> GetByUserId(int userId)
        {
            const string getSql = @"
select  lbb.ID, lbb.Name, lbb.UserId, lbb.IsEnable, lbb.CreateBy, lbb.CreateTime
from    LC_BillBooks lbb ( nolock )
where   lbb.IsEnable = 1
        and lbb.UserId = @UserId";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, getSql, dbParameters, new BillBooksRowMapper());
        }

        #region  Nested type: BillBooksRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class BillBooksRowMapper : IDataTableRowMapper<BillBooksDTO>
        {
            public BillBooksDTO MapRow(DataRow dataReader)
            {
                var result = new BillBooksDTO();
                object obj;
                obj = dataReader["ID"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.ID = int.Parse(obj.ToString());
                }
                result.Name = dataReader["Name"].ToString();
                obj = dataReader["UserId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.UserId = int.Parse(obj.ToString());
                }
                obj = dataReader["IsEnable"];
                if (obj != null && obj != DBNull.Value)
                {
                    if (obj.ToString() == 1.ToString() || obj.ToString().ToLower() == "true")
                    {
                        result.IsEnable = true;
                    }
                    else
                    {
                        result.IsEnable = false;
                    }
                }
                result.CreateBy = dataReader["CreateBy"].ToString();
                obj = dataReader["CreateTime"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.CreateTime = DateTime.Parse(obj.ToString());
                }

                return result;
            }
        }

        #endregion

    }
}
