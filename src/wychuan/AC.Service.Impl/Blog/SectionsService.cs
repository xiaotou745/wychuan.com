using System.Collections.Generic;
using AC.Dao;
using AC.Dao.Blog;
using AC.Service.Blog;
using AC.Service.DTO.Blog;
using AC.Transaction.Common;

namespace AC.Service.Impl.Blog
{
    public class SectionsService : ISectionsService
    {
        private readonly SectionsDao sectionsDao;
        private readonly SectionTagsDao sectionTagDao;
        private readonly IBlogTagsService tagDao;

        public SectionsService()
        {
            sectionsDao = new SectionsDao();
            sectionTagDao = new SectionTagsDao();
            tagDao = new BlogTagsService();
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        public int Create(Sections sections)
        {
            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                //插件section
                int id = sectionsDao.Insert(sections);
                //插件section tags(tag已存在)
                if (sections.TagIds != null && sections.TagIds.Count > 0)
                {
                    foreach (var tagId in sections.TagIds)
                    {
                        var st = new SectionsTags {SectionId = id, TagId = tagId};
                        sectionTagDao.Insert(st);
                    }
                }
                //tags不存在，先创建tag，然后再插入section tags.
                if (!string.IsNullOrEmpty(sections.NewTags))
                {
                    string[] newTags = sections.NewTags.Split(new string[] {","},
                        System.StringSplitOptions.RemoveEmptyEntries);
                    foreach (var newTag in newTags)
                    {
                        BlogTagsDTO tagInfo = new BlogTagsDTO {UserId = sections.UserId, TagName = newTag};
                        int tagId = tagDao.Create(tagInfo);
                        var st = new SectionsTags {SectionId = id, TagId = tagId};
                        sectionTagDao.Insert(st);
                    }
                }
                unitOfWork.Complete();
                return id;
            }
        }

        /// <summary>
        /// 修改一条记录
        /// </summary>
        public void Modify(Sections sections)
        {
            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                sectionsDao.Update(sections);
                //清除所有的已有tag
                sectionTagDao.DeleteBySectionId(sections.Id);
                //插件section tags(tag已存在)
                if (sections.TagIds != null && sections.TagIds.Count > 0)
                {
                    foreach (var tagId in sections.TagIds)
                    {
                        var st = new SectionsTags {SectionId = sections.Id, TagId = tagId};
                        sectionTagDao.Insert(st);
                    }
                }
                //tags不存在，先创建tag，然后再插入section tags.
                if (!string.IsNullOrEmpty(sections.NewTags))
                {
                    string[] newTags = sections.NewTags.Split(new string[] {","},
                        System.StringSplitOptions.RemoveEmptyEntries);
                    foreach (var newTag in newTags)
                    {
                        BlogTagsDTO tagInfo = new BlogTagsDTO {UserId = sections.UserId, TagName = newTag};
                        int tagId = tagDao.Create(tagInfo);
                        var st = new SectionsTags {SectionId = sections.Id, TagId = tagId};
                        sectionTagDao.Insert(st);
                    }
                }
                unitOfWork.Complete();
            }
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        public void Remove(int id)
        {
            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                sectionTagDao.DeleteBySectionId(id);
                sectionsDao.Delete(id);
                unitOfWork.Complete();
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sections GetById(int id)
        {
            Sections section = sectionsDao.GetById(id);
            if (section == null)
            {
                return null;
            }
            section.Tags = sectionTagDao.GetBySectionId(id);
            return section;
        }

        public Sections GetBySectionId(string sectionId)
        {
            return sectionsDao.GetBySectionId(sectionId);
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        public IList<Sections> Query(SectionsQueryInfo queryInfo)
        {
            return sectionsDao.Query(queryInfo);
        }
    }
}