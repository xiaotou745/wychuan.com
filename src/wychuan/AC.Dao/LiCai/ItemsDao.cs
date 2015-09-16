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
    public class ItemsDao : DaoBase
    {
        public void InitUserItems(int userId)
        {
            const string initSql = @"
insert into LC_Items ( UserId, Type, Name, Level, IsDefault, IsEnable )
select @UserId,li.Type,li.Name,li.Level,li.IsDefault,li.IsEnable
from LC_Items li(nolock)
where li.UserId=0";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, initSql, dbParameters);
        }

        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(ItemDTO lCItemsDTO)
        {
            const string insertSql = @"
insert into LC_Items(UserId,Type,Name,Level,IsDefault)
values(@UserId,@Type,@Name,@Level,@IsDefault)

select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("UserId", lCItemsDTO.UserId);
            dbParameters.AddWithValue("Type", lCItemsDTO.Type);
            dbParameters.AddWithValue("Name", lCItemsDTO.Name);
            dbParameters.AddWithValue("Level", lCItemsDTO.Level);
            dbParameters.AddWithValue("IsDefault", lCItemsDTO.IsDefault);

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
        public IList<ItemDTO> GetByUserId(int userId)
        {
            const string getByUserIdSql = @"
select  li.Id, li.UserId, li.Type, li.Name, li.Level, li.IsDefault, li.LastUseTime, li.IsEnable
from    LC_Items li ( nolock )
where   li.IsEnable = 1
        and li.UserId = @UserId
order by li.IsDefault desc, li.Level ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, getByUserIdSql, dbParameters, new ItemsRowMapper());
        }

        public IList<ItemDTO> GetByUserId(int userId, ItemType type)
        {
            const string getByUserIdSql = @"
select  li.Id, li.UserId, li.Type, li.Name, li.Level, li.IsDefault, li.LastUseTime, li.IsEnable
from    LC_Items li ( nolock )
where   li.IsEnable = 1
        and li.UserId = @UserId
        and li.Type=@Type
order by li.IsDefault, li.Level ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);
            dbParameters.Add("Type", DbType.Int32, 4).Value = type.GetHashCode();

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, getByUserIdSql, dbParameters, new ItemsRowMapper());
        }
        /// <summary>
        /// 绑定对象
        /// </summary>
        private class ItemsRowMapper : IDataTableRowMapper<ItemDTO>
        {
            public ItemDTO MapRow(DataRow dataReader)
            {
                var result = new ItemDTO();
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
                obj = dataReader["Type"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Type = (ItemType)Enum.Parse(typeof (ItemType), obj.ToString());
                }
                result.Name = dataReader["Name"].ToString();
                obj = dataReader["Level"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Level = int.Parse(obj.ToString());
                }
                obj = dataReader["IsDefault"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.IsDefault = int.Parse(obj.ToString());
                }
                obj = dataReader["LastUseTime"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.LastUseTime = DateTime.Parse(obj.ToString());
                }
                
                return result;
            }
        }

    }
}
