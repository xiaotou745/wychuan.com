using System;
using System.Collections.Generic;
using System.Data;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Service.DTO.User;
using AC.Util;

namespace AC.Dao.User
{
    public class RoleDao : DaoBase
    {
        #region Insert

        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(RoleDTO roleDTO)
        {
            const string INSERT_SQL = @"
insert into Role(Name)
values(@Name)
 
select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Name", roleDTO.Name);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, INSERT_SQL, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());
        }

        #endregion

        #region Update
        /// <summary>
        /// 更新一条记录
        /// </summary>
        public void Update(RoleDTO roleDTO)
        {
            const string UPDATE_SQL = @"
update  Role
set  Name=@Name
where  Id=@Id ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Id", roleDTO.Id);
            dbParameters.AddWithValue("Name", roleDTO.Name);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, UPDATE_SQL, dbParameters);
        }
        #endregion

        #region GetAll

        public IList<RoleDTO> GetAll()
        {
            const string sqlText = @"select Id, Name from [Role] r(nolock)";
            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, sqlText, new RoleRowMapper());
        }
        #endregion

        #region SavePrivilege

        public void SavePrivilege(int roleId, int menuId)
        {
            const string SQL_TEXT = @"
insert into RolePrivilege ( RoleId, MenuId )
values  ( @RoleId, @MenuId)";

            var dbParameters = DbHelper.CreateDbParameters();
            dbParameters.Add("RoleId", DbType.Int32, 4).Value = roleId;
            dbParameters.Add("MenuId", DbType.Int32, 4).Value = menuId;

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, SQL_TEXT, dbParameters);
        }
        #endregion

        #region  Nested type: RoleRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class RoleRowMapper : IDataTableRowMapper<RoleDTO>
        {
            public RoleDTO MapRow(DataRow dataReader)
            {
                var result = new RoleDTO();
                object obj = dataReader["Id"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Id = int.Parse(obj.ToString());
                }
                result.Name = dataReader["Name"].ToString();

                return result;
            }
        }

        #endregion
        #region SavePrivilege
        public void DeletePrivilege(int roleId)
        {
            const string SQL_TEXT = @"delete from RolePrivilege where RoleId=@RoleId";

            var dbParameters = DbHelper.CreateDbParameters("RoleId", DbType.Int32, 4, roleId);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, SQL_TEXT, dbParameters);
        }
        #endregion

        #region GetPrivilege

        public IList<int> GetPrivilege(int roleId)
        {
            const string sqlText =
                "select  rp.Id, rp.RoleId, rp.MenuId from RolePrivilege rp(nolock) where rp.RoleId=@RoleId";
            var dbParameters = DbHelper.CreateDbParameters("RoleId", DbType.Int32, 4, roleId);
            return DbHelper.QueryWithRowMapperDelegate(ConnStringOfAchuan, sqlText, dbParameters,
                (row => ParseHelper.ToInt(row["MenuId"])));
        }
        #endregion

        public void Delete(int id)
        {
            const string DELETE_SQL = "delete from Role where Id=@Id";

            var dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, DELETE_SQL, dbParameters);
        }
    }
}