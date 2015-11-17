using System;
using System.Collections.Generic;
using System.Data;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Service.DTO.User;
using AC.Util;

namespace AC.Dao.User
{
    public class MenuDao : DaoBase
    {
        #region Insert

        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(MenuDTO menuDTO)
        {
            const string INSERT_SQL = @"
insert into Menu(Name,ParentId,Url,Icon,OrderBy,Controller,Action)
values(@Name,@ParentId,@Url,@Icon,@OrderBy,@Controller,@Action)
 
select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Name", menuDTO.Name);
            dbParameters.AddWithValue("ParentId", menuDTO.ParentId);
            dbParameters.AddWithValue("Url", menuDTO.Url??string.Empty);
            dbParameters.AddWithValue("Icon", menuDTO.Icon ?? string.Empty);
            dbParameters.AddWithValue("OrderBy", menuDTO.OrderBy);
            dbParameters.AddWithValue("Controller", menuDTO.Controller ?? string.Empty);
            dbParameters.AddWithValue("Action", menuDTO.Action ?? string.Empty);

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
        public void Update(MenuDTO menuDTO)
        {
            const string UPDATE_SQL = @"
update  Menu
set  Name=@Name,ParentId=@ParentId,Url=@Url,Icon=@Icon,OrderBy=@OrderBy,Controller=@Controller,Action=@Action
where  Id=@Id ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Id", menuDTO.Id);
            dbParameters.AddWithValue("Name", menuDTO.Name);
            dbParameters.AddWithValue("ParentId", menuDTO.ParentId);
            dbParameters.AddWithValue("Url", menuDTO.Url??string.Empty);
            dbParameters.AddWithValue("Icon", menuDTO.Icon??string.Empty);
            dbParameters.AddWithValue("OrderBy", menuDTO.OrderBy);
            dbParameters.AddWithValue("Controller", menuDTO.Controller ?? string.Empty);
            dbParameters.AddWithValue("Action", menuDTO.Action ?? string.Empty);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, UPDATE_SQL, dbParameters);
        }

        #endregion

        public IList<MenuDTO> GetAll()
        {
            const string sqlText = @"
select  m.Id, m.Name, m.ParentId, m.Url, m.Icon, m.OrderBy,Action,Controller
from    Menu m ( nolock )";
            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, sqlText, new MenuRowMapper());
        }

        #region Delete

        /// <summary>
        /// 删除一条记录
        /// </summary>
        public void Delete(int id)
        {
            const string DELETE_SQL = @"delete from Menu where Id=@Id ";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, DELETE_SQL, dbParameters);
        }

        #endregion

        #region  Nested type: MenuRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class MenuRowMapper : IDataTableRowMapper<MenuDTO>
        {
            public MenuDTO MapRow(DataRow dataReader)
            {
                var result = new MenuDTO();
                object obj = dataReader["Id"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.Id = int.Parse(obj.ToString());
                }
                result.Name = dataReader["Name"].ToString();
                obj = dataReader["ParentId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.ParentId = int.Parse(obj.ToString());
                }
                result.Url = dataReader["Url"].ToString();
                result.Icon = dataReader["Icon"].ToString();
                result.OrderBy = ParseHelper.ToInt(dataReader["OrderBy"]);
                result.Controller = dataReader["Controller"].ToString();
                result.Action = dataReader["Action"].ToString();

                return result;
            }
        }

        #endregion

        #region GetRolePrivilege
        public IList<MenuDTO> GetRolePrivilege(int roleId)
        {
            const string SQL_TEXT = @"
select  m.Id, m.Name, m.ParentId, m.Url, m.Icon, m.OrderBy, m.Controller, m.Action
from    Menu m ( nolock )
        join RolePrivilege rp ( nolock ) on m.Id = rp.MenuId
where   rp.RoleId = @RoleId";

            var dbParameters = DbHelper.CreateDbParameters("RoleId", DbType.Int32, 4, roleId);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, SQL_TEXT, dbParameters, new MenuRowMapper());
        }
        #endregion
    }
}