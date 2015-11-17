using System;
using System.Collections.Generic;
using System.Data;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Helper;
using AC.Service.DTO.User;

namespace AC.Dao.User
{
    public class UserDao : DaoBase
    {
        #region Insert
        public int Insert(UserDTO user)
        {
            const string sql = @"
insert into [User]
        ( LoginName ,
          LoginPwd,
          RegisterTime
        )
values  ( @LoginName ,
          @LoginPwd,
          getdate()
        )
select @@identity";
            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("LoginName", user.LoginName);
            dbParameters.AddWithValue("LoginPwd", user.LoginPassword);
            object executeScalar = DbHelper.ExecuteScalar(ConnStringOfAchuan, sql, dbParameters);
            return ParseHelper.ToInt(executeScalar);
        }

        #endregion
        public UserDTO GetByName(string userName)
        {
            const string sql = @"
select  u.Id ,
        u.LoginName ,
        u.LoginPwd ,
        u.IsEnable ,
        u.RegisterTime,
        u.IsDisable,
        u.RoleIds
from [User] u(nolock)
where u.LoginName=@LoginName
	and u.IsEnable=1";
            IDbParameters dbParameters = DbHelper.CreateDbParameters("LoginName", DbType.String, 250, userName);

            return DbHelper.QueryForObject(ConnStringOfAchuan, sql, dbParameters, new UserRowMapper());
        }

        #region GetAll
        public IList<UserDTO> GetAll()
        {
            const string SQL = @"
select  u.Id ,
        u.LoginName ,
        u.LoginPwd ,
        u.IsEnable ,
        u.RegisterTime,
        u.IsDisable,
        u.RoleIds
from [User] u(nolock)
where u.IsEnable=1";

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, SQL, new UserRowMapper());
        }
        #endregion

        #region Disable

        public void Disable(int userId, bool isDisable)
        {
            const string SQL_TEXT = "update [User] set IsDisable=@Disable where Id=@Id";

            var dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, userId);
            dbParameters.Add("Disable", DbType.Boolean).Value = isDisable;

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, SQL_TEXT, dbParameters);
        }
        #endregion

        #region Delete

        public void Delete(int userId)
        {
            const string SQL_TEXT = "update [User] set IsEnable=0 where Id=@Id";

            var dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, userId);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, SQL_TEXT, dbParameters);
        }
        #endregion

        #region SetRoles

        public void SetRoles(int userId, string roleIds)
        {
            const string SQL_TEXT = "update [User] set RoleIds=@RoleIds where Id=@Id";

            var dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, userId);
            dbParameters.Add("RoleIds", DbType.String, 50).Value = roleIds;

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, SQL_TEXT, dbParameters);
        }
        #endregion

        #region  Nested type: UserRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class UserRowMapper : IDataTableRowMapper<UserDTO>
        {
            public UserDTO MapRow(DataRow dataReader)
            {
                var result = new UserDTO();
                object obj = dataReader["Id"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Id = int.Parse(obj.ToString());
                }
                result.LoginName = dataReader["LoginName"].ToString();
                result.LoginPassword = dataReader["LoginPwd"].ToString();
                obj = dataReader["RegisterTime"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.RegisterTime = DateTime.Parse(obj.ToString());
                }
                obj = dataReader["IsDisable"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.IsDisable = bool.Parse(obj.ToString());
                }
                result.RoleIds = dataReader["RoleIds"].ToString();
                return result;
            }
        }

        #endregion
    }
}