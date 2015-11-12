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
    public class CategoryDao:DaoBase
    {
        public int Insert(CategoryDTO category)
        {
            const string insertSql = @"
insert into LC_Category(UserId,ParentId,Name,InOutType,InitKey,IsCommonUse,OrderBy)
values(@UserId,@ParentId,@Name,@InOutType,@InitKey,@IsCommonUse,@OrderBy)

select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("UserId", category.UserId);
            dbParameters.AddWithValue("ParentId", category.ParentId);
            dbParameters.AddWithValue("Name", category.Name);
            dbParameters.AddWithValue("InOutType", category.InOutType.GetHashCode());
            dbParameters.AddWithValue("InitKey", category.InitKey);
            dbParameters.AddWithValue("IsCommonUse", category.IsCommonUse);
            dbParameters.AddWithValue("OrderBy", category.OrderBy);


            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, insertSql, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }

        public void Rename(int id, string name)
        {
            const string sqlText = @"update LC_Category set Name=@Name where ID=@Id";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.Add("Id", DbType.Int32, 4).Value = id;
            dbParameters.Add("Name", DbType.String, 50).Value = name;

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, sqlText, dbParameters);
        }

        public void InitUser(int userId)
        {
            const string initSql = @"
insert into LC_Category ( UserId, ParentId, Name, InOutType, InitKey, IsCommonUse, OrderBy )
select @UserId,lc.ParentId,lc.Name,lc.InOutType,lc.InitKey,lc.IsCommonUse,lc.OrderBy
from LC_Category lc(nolock)
where lc.UserId=0

update cu
set ParentId=lc.ID
from LC_Category cu
	join LC_Category lc on lc.UserId=@UserId and lc.ParentId=0 and cu.ParentId=lc.InitKey
where cu.UserId=@UserId
and cu.ParentId>0";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, initSql, dbParameters);
        }

        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        public IList<CategoryDTO> GetByUserId(int userId)
        {
            const string getByUserIdSql = @"
select  lc.ID, lc.UserId, lc.ParentId, lc.Name, lc.InOutType, lc.InitKey, lc.IsCommonUse, lc.OrderBy, lc.IsEnable
from    LC_Category lc ( nolock )
where   lc.IsEnable = 1
        and lc.UserId = @UserId";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, getByUserIdSql, dbParameters, new CategoryRowMapper());
        }

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class CategoryRowMapper : IDataTableRowMapper<CategoryDTO>
        {
            public CategoryDTO MapRow(DataRow dataReader)
            {
                var result = new CategoryDTO();
                object obj = dataReader["ID"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Id = int.Parse(obj.ToString());
                }
                obj = dataReader["UserId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.UserId = int.Parse(obj.ToString());
                }
                obj = dataReader["ParentId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.ParentId = int.Parse(obj.ToString());
                }
                result.Name = dataReader["Name"].ToString();
                obj = dataReader["InOutType"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.InOutType = (CategoryType) Enum.Parse(typeof (CategoryType), obj.ToString());
                }
                obj = dataReader["IsCommonUse"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.IsCommonUse = int.Parse(obj.ToString());
                }
                obj = dataReader["OrderBy"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.OrderBy = int.Parse(obj.ToString());
                }

                return result;
            }
        }

        public void Delete(int id)
        {
            const string sqlText = @"update LC_Category set IsEnable=0 where ID=@Id";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, sqlText, dbParameters);
        }

        public CategoryDTO GetByName(int userId, int type, string name)
        {
            const string SQL_TEXT = @"
select  lc.ID, lc.UserId, lc.ParentId, lc.Name, lc.InOutType, lc.InitKey, lc.IsCommonUse, lc.OrderBy, lc.IsEnable
from    LC_Category lc ( nolock )
where   lc.IsEnable = 1
        and lc.UserId = @UserId
        and lc.InOutType = @InOutType
        and lc.Name = @Name";

            var dbParameters = DbHelper.CreateDbParameters();
            dbParameters.Add("UserId", DbType.Int32, 4).Value = userId;
            dbParameters.Add("InOutType", DbType.Int32, 4).Value = type;
            dbParameters.Add("Name", DbType.String, 50).Value = name;

            return DbHelper.QueryForObject(ConnStringOfAchuan, SQL_TEXT, dbParameters, new CategoryRowMapper());
        }
    }
}
