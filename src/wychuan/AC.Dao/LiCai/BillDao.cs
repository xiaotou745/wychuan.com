using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Data.Core;
using AC.Service.DTO.LiCai;

namespace AC.Dao.LiCai
{
    public class BillDao : DaoBase
    {
        #region Insert
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(BillDTO bill)
        {
            const string insertSql = @"
insert into LC_BillDetails(UserId,BookId,Price,FirstCategoryId,FirstCategory,SecondCategoryId,SecondCategory,AccountId,ToAccountId,ConsumeTime,Remark,DetailType,BusinessId,Business,MemberId,Member,ProjectId,Project,RefundNotice,RefundTime,BaoXiao)
values(@UserId,@BookId,@Price,@FirstCategoryId,@FirstCategory,@SecondCategoryId,@SecondCategory,@AccountId,@ToAccountId,@ConsumeTime,@Remark,@DetailType,@BusinessId,@Business,@MemberId,@Member,@ProjectId,@Project,@RefundNotice,@RefundTime,@BaoXiao)

select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("UserId", bill.UserId);
            dbParameters.AddWithValue("BookId", bill.BookId);
            dbParameters.AddWithValue("Price", bill.Price);
            dbParameters.AddWithValue("FirstCategoryId", bill.FirstCategoryId);
            dbParameters.AddWithValue("FirstCategory", bill.FirstCategory);
            dbParameters.AddWithValue("SecondCategoryId", bill.SecondCategoryId);
            dbParameters.AddWithValue("SecondCategory", bill.SecondCategory);
            dbParameters.AddWithValue("AccountId", bill.AccountId);
            dbParameters.AddWithValue("ToAccountId", bill.ToAccountId);
            dbParameters.AddWithValue("ConsumeTime", bill.ConsumeTime);
            dbParameters.AddWithValue("Remark", bill.Remark);
            dbParameters.AddWithValue("DetailType", bill.DetailType);
            dbParameters.AddWithValue("BusinessId", bill.BusinessId);
            dbParameters.AddWithValue("Business", bill.Business);
            dbParameters.AddWithValue("MemberId", bill.MemberId);
            dbParameters.AddWithValue("Member", bill.Member);
            dbParameters.AddWithValue("ProjectId", bill.ProjectId);
            dbParameters.AddWithValue("Project", bill.Project);
            dbParameters.AddWithValue("RefundNotice", bill.RefundNotice);
            dbParameters.AddWithValue("RefundTime", bill.RefundTime);
            dbParameters.AddWithValue("BaoXiao", bill.BaoXiao);

            object result = DbHelper.ExecuteScalar(ConnStringOfAchuan, insertSql, dbParameters);
            if (result == null)
            {
                return 0;
            }
            return int.Parse(result.ToString());

        }

        #endregion
    }
}
