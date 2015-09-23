using System;
using AC.Dao.Tools;
using AC.Service.DTO.Tools;
using AC.Service.Tools;
using AC.Util;

namespace AC.Service.Impl.Tools
{
    public class CompanyTimelineService : ICompanyTimelineService
    {
        private readonly CompanyTimelineDao timelineDao;

        public CompanyTimelineService()
        {
            timelineDao = new CompanyTimelineDao();
        }

        public int Create(CompanyTimelineDTO timeline)
        {
            AssertUtils.ArgumentNotNull(timeline, "timeline");

            return timelineDao.Insert(timeline);
        }
    }
}