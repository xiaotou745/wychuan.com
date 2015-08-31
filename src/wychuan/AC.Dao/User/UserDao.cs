using System.Data;
using AC.Data.Core;
using AC.Helper;
using AC.Service.DTO.User;

namespace AC.Dao.User
{
    public class UserDao : DaoBase
    {
        public int Insert(UserDTO user)
        {
            const string sql = @"
insert into [User]
        ( LoginName ,
          LoginPwd 
        )
values  ( @LoginName ,
          @LoginPwd 
        )
select @@identity";
            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("LoginName", user.LoginName);
            dbParameters.AddWithValue("LoginPwd", user.LoginPassword);
            object executeScalar = DbHelper.ExecuteScalar(ConnStringOfAchuan, sql, dbParameters);
            return ParseHelper.ToInt(executeScalar);
        }

        public UserDTO GetByName(string userName)
        {
            const string sql = @"
select  u.Id ,
        u.LoginName ,
        u.LoginPwd ,
        u.IsEnable ,
        u.RegisterTime
from [User] u(nolock)
where u.LoginName=@LoginName
	and u.IsEnable=1";
            IDbParameters dbParameters = DbHelper.CreateDbParameters("LoginName", DbType.String, 250, userName);

            return DbHelper.QueryForObjectDelegate(ConnStringOfAchuan, sql, dbParameters, (row => new UserDTO
            {
                Id = ParseHelper.ToInt(row["Id"]),
                LoginName = row["LoginName"].ToString(),
                LoginPassword = row["LoginPwd"].ToString(),
            }));
        }
    }
}