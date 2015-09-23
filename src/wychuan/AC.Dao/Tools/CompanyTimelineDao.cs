using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Data.Core;
using AC.Service.DTO.Tools;

namespace AC.Dao.Tools
{
    public class CompanyTimelineDao : DaoBase
    {
        #region Insert
        /// <summary>
        /// 增加一条记录
        /// </summary>
        public int Insert(CompanyTimelineDTO companyTimeLineDTO)
        {
            const string insertSql = @"
insert into CompanyTimeLine(CompanyId,EventName,EventContent,EventTime)
values(@CompanyId,@EventName,@EventContent,@EventTime)

select @@IDENTITY";

            IDbParameters dbParameters = DbHelper.CreateDbParameters();
            dbParameters.AddWithValue("CompanyId", companyTimeLineDTO.CompanyId);
            dbParameters.AddWithValue("EventName", companyTimeLineDTO.EventName);
            dbParameters.AddWithValue("EventContent", companyTimeLineDTO.EventContent);
            dbParameters.AddWithValue("EventTime", companyTimeLineDTO.EventTime);


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
