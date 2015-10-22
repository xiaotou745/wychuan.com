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
    public class BillTemplateDao:DaoBase
    {
        #region ILCBillTemplateRepos  Members

        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(BillTemplateDTO lCBillTemplateDTO)
        {
            const string insertSql = @"
insert into LC_BillTemplate(Name,UserId,BookId,FirstCategoryId,FirstCategory,SecondCategoryId,SecondCategory,
    AccountId,AccountDesc,Remark,DetailType,BusinessId,Business,MemberId,Member,ProjectId,Project,
    BaoXiao)
values(@Name,@UserId,@BookId,@FirstCategoryId,@FirstCategory,@SecondCategoryId,@SecondCategory,
    @AccountId,@AccountDesc,@Remark,1,@BusinessId,@Business,@MemberId,@Member,@ProjectId,@Project,
    @BaoXiao)

select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("Name", lCBillTemplateDTO.Name);
            dbParameters.AddWithValue("UserId", lCBillTemplateDTO.UserId);
            dbParameters.AddWithValue("BookId", lCBillTemplateDTO.BookId);
            dbParameters.AddWithValue("FirstCategoryId", lCBillTemplateDTO.FirstCategoryId);
            dbParameters.AddWithValue("FirstCategory", lCBillTemplateDTO.FirstCategory);
            dbParameters.AddWithValue("SecondCategoryId", lCBillTemplateDTO.SecondCategoryId);
            dbParameters.AddWithValue("SecondCategory", lCBillTemplateDTO.SecondCategory);
            dbParameters.AddWithValue("AccountId", lCBillTemplateDTO.AccountId);
            dbParameters.AddWithValue("AccountDesc", lCBillTemplateDTO.AccountDesc);
            dbParameters.AddWithValue("Remark", lCBillTemplateDTO.Remark);
            dbParameters.AddWithValue("BusinessId", lCBillTemplateDTO.BusinessId);
            dbParameters.AddWithValue("Business", lCBillTemplateDTO.Business);
            dbParameters.AddWithValue("MemberId", lCBillTemplateDTO.MemberId);
            dbParameters.AddWithValue("Member", lCBillTemplateDTO.Member);
            dbParameters.AddWithValue("ProjectId", lCBillTemplateDTO.ProjectId);
            dbParameters.AddWithValue("Project", lCBillTemplateDTO.Project);
            dbParameters.AddWithValue("BaoXiao", lCBillTemplateDTO.BaoXiao);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, insertSql, dbParameters);
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
        public void Update(BillTemplateDTO lCBillTemplateDTO)
        {
            const string updateSql = @"
update  LC_BillTemplate
set  Name=@Name,BookId=@BookId,FirstCategoryId=@FirstCategoryId,FirstCategory=@FirstCategory,
    SecondCategoryId=@SecondCategoryId,SecondCategory=@SecondCategory,AccountId=@AccountId,AccountDesc=@AccountDesc,
    Remark=@Remark,BusinessId=@BusinessId,Business=@Business,MemberId=@MemberId,Member=@Member,
    ProjectId=@ProjectId,Project=@Project,BaoXiao=@BaoXiao
where  ID=@ID";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("ID", lCBillTemplateDTO.ID);
            dbParameters.AddWithValue("Name", lCBillTemplateDTO.Name);
            dbParameters.AddWithValue("BookId", lCBillTemplateDTO.BookId);
            dbParameters.AddWithValue("FirstCategoryId", lCBillTemplateDTO.FirstCategoryId);
            dbParameters.AddWithValue("FirstCategory", lCBillTemplateDTO.FirstCategory);
            dbParameters.AddWithValue("SecondCategoryId", lCBillTemplateDTO.SecondCategoryId);
            dbParameters.AddWithValue("SecondCategory", lCBillTemplateDTO.SecondCategory);
            dbParameters.AddWithValue("AccountId", lCBillTemplateDTO.AccountId);
            dbParameters.AddWithValue("AccountDesc", lCBillTemplateDTO.AccountDesc);
            dbParameters.AddWithValue("Remark", lCBillTemplateDTO.Remark);
            dbParameters.AddWithValue("BusinessId", lCBillTemplateDTO.BusinessId);
            dbParameters.AddWithValue("Business", lCBillTemplateDTO.Business);
            dbParameters.AddWithValue("MemberId", lCBillTemplateDTO.MemberId);
            dbParameters.AddWithValue("Member", lCBillTemplateDTO.Member);
            dbParameters.AddWithValue("ProjectId", lCBillTemplateDTO.ProjectId);
            dbParameters.AddWithValue("Project", lCBillTemplateDTO.Project);
            dbParameters.AddWithValue("BaoXiao", lCBillTemplateDTO.BaoXiao);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, updateSql, dbParameters);
        }

        #endregion

        #region Delete
        /// <summary>
        /// 删除一条记录
        /// </summary>
        public void Delete(int id)
        {
            const string deleteSql = @"update LC_BillTemplate set Enable=0 where ID=@Id";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("Id", DbType.Int32, 4, id);

            DbHelper.ExecuteNonQuery(ConnStringOfAchuan, deleteSql, dbParameters);
        }

        #endregion

        #region GetByUserId

        public IList<BillTemplateDTO> GetByUserId(int userId)
        {
            const string sql =
                @"
select  lbt.ID, lbt.Name, lbt.UserId, lbt.BookId, lbt.FirstCategoryId, lbt.FirstCategory, lbt.SecondCategoryId,
        lbt.SecondCategory, lbt.AccountId, lbt.AccountDesc, lbt.Remark, lbt.DetailType, lbt.BusinessId, lbt.Business,
        lbt.MemberId, lbt.Member, lbt.ProjectId, lbt.Project, lbt.BaoXiao, lbt.Enable
from LC_BillTemplate lbt(nolock)
where lbt.Enable=1
	and lbt.UserId=@UserId";

            IDbParameters dbParameters = DbHelper.CreateDbParameters("UserId", DbType.Int32, 4, userId);

            return DbHelper.QueryWithRowMapper(ConnStringOfAchuan, sql, dbParameters, new BillTemplateRowMapper());
        }

        #endregion

        #region  Nested type: LC_BillTemplateRowMapper

        /// <summary>
        /// 绑定对象
        /// </summary>
        private class BillTemplateRowMapper : IDataTableRowMapper<BillTemplateDTO>
        {
            public BillTemplateDTO MapRow(DataRow dataReader)
            {
                var result = new BillTemplateDTO();
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
                obj = dataReader["BookId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.BookId = int.Parse(obj.ToString());
                }
                obj = dataReader["FirstCategoryId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.FirstCategoryId = int.Parse(obj.ToString());
                }
                result.FirstCategory = dataReader["FirstCategory"].ToString();
                obj = dataReader["SecondCategoryId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.SecondCategoryId = int.Parse(obj.ToString());
                }
                result.SecondCategory = dataReader["SecondCategory"].ToString();
                obj = dataReader["AccountId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.AccountId = int.Parse(obj.ToString());
                }
                result.AccountDesc = dataReader["AccountDesc"].ToString();
                result.Remark = dataReader["Remark"].ToString();
                obj = dataReader["DetailType"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.DetailType = int.Parse(obj.ToString());
                }
                obj = dataReader["BusinessId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.BusinessId = int.Parse(obj.ToString());
                }
                result.Business = dataReader["Business"].ToString();
                obj = dataReader["MemberId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.MemberId = int.Parse(obj.ToString());
                }
                result.Member = dataReader["Member"].ToString();
                obj = dataReader["ProjectId"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.ProjectId = int.Parse(obj.ToString());
                }
                result.Project = dataReader["Project"].ToString();
                obj = dataReader["BaoXiao"];
                if (obj != null && obj != DBNull.Value)
                {
                    result.BaoXiao = int.Parse(obj.ToString());
                }
                
                return result;

            }
        }

        #endregion

    }
}
