using System.Collections.Generic;
using AC.Dao.Blog;
using AC.Service.Blog;
using AC.Service.DTO.Blog;

namespace AC.Service.Impl.Blog
{
    public class SectionAnchorsService : ISectionAnchorsService
    {
        private readonly SectionAnchorDao anchorDao;

        public SectionAnchorsService()
        {
            this.anchorDao = new SectionAnchorDao();
        }

        public int Create(SectionAnchors sectionAnchors)
        {
            return anchorDao.Insert(sectionAnchors);
        }

        public void Modify(SectionAnchors sectionAnchors)
        {
            anchorDao.Update(sectionAnchors);
        }

        public void Remove(int id)
        {
            anchorDao.Delete(id);
        }

        public IList<SectionAnchors> GetBySectionId(int sectionId)
        {
            return anchorDao.GetBySectionId(sectionId);
        }

        public IList<SectionAnchors> GetBySectionIds(IList<int> sectionIds)
        {
            return anchorDao.GetBySectionIds(sectionIds);
        }
    }
}