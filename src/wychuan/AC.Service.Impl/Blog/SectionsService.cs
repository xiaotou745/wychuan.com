using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AC.Dao;
using AC.Dao.Blog;
using AC.IO;
using AC.Page;
using AC.Service.Blog;
using AC.Service.DTO.Blog;
using AC.Transaction.Common;
using AC.Util;

namespace AC.Service.Impl.Blog
{
    public class SectionsService : ISectionsService
    {
        private readonly SectionsDao sectionsDao;
        private readonly SectionTagsDao sectionTagDao;
        private readonly IBlogTagsService tagDao;
        private readonly SectionAnchorDao anchorDao;

        public SectionsService()
        {
            sectionsDao = new SectionsDao();
            sectionTagDao = new SectionTagsDao();
            tagDao = new BlogTagsService();
            anchorDao = new SectionAnchorDao();
        }

        #region  Create, Modify,Remove,Get,Query

        /// <summary>
        /// 新增一条记录
        /// </summary>
        public int Create(Sections sections)
        {
            Sections section = GetBySectionId(sections.SectionId);
            if (section != null)
            {
                throw new SectionIdExistsException();
            }
            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                //插件section
                int id = sectionsDao.Insert(sections);
                if (sections.Tags != null)
                {
                    foreach (SectionsTags sectionTag in sections.Tags)
                    {
                        sectionTag.SectionId = id;
                        if (sectionTag.TagId == 0) //tags不存在，先创建tag，然后再插入section tags.
                        {
                            BlogTagsDTO tagInfo = new BlogTagsDTO
                            {
                                UserId = sections.UserId,
                                TagName = sectionTag.TagName
                            };
                            sectionTag.TagId = tagDao.Create(tagInfo);
                        }
                        sectionTagDao.Insert(sectionTag);
                    }
                }

                SaveHtmlFile(sections);
                unitOfWork.Complete();
                return id;
            }
        }

        public void SaveHtmlFile(Sections section)
        {
            string basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string savePath = string.Empty;
            if (section.ParentId == 0)
            {
                savePath = Path.Combine(basePath, "sections", section.SectionId, section.SectionId + ".html");
            }
            else
            {
                Sections parentSection = GetById(section.ParentId);
                savePath = Path.Combine(basePath, "sections", parentSection.SectionId, section.SectionId + ".html");
            }
            FilesHelper.CreateFileDir(savePath);
            FileIO.SaveTextFile(savePath, section.Content, System.Text.Encoding.UTF8);
            //AppLogger.Logger.Info(System.AppDomain.CurrentDomain.BaseDirectory);
            //AppLogger.Logger.Info(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            //AppLogger.Logger.Info(HttpContext.Current.Request.PhysicalApplicationPath);
        }

        /// <summary>
        /// 修改一条记录
        /// </summary>
        public void Modify(Sections sections)
        {
            Sections section = GetBySectionId(sections.SectionId);
            if (section != null && sections.Id != section.Id)
            {
                throw new SectionIdExistsException();
            }
            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                sectionsDao.Update(sections);
                //清除所有的已有tag
                sectionTagDao.DeleteBySectionId(sections.Id);
                if (sections.Tags != null)
                {
                    foreach (SectionsTags sectionTag in sections.Tags)
                    {
                        sectionTag.SectionId = sections.Id;
                        if (sectionTag.TagId == 0) //tags不存在，先创建tag，然后再插入section tags.
                        {
                            BlogTagsDTO tagInfo = new BlogTagsDTO
                            {
                                UserId = sections.UserId,
                                TagName = sectionTag.TagName
                            };
                            sectionTag.TagId = tagDao.Create(tagInfo);
                        }
                        sectionTagDao.Insert(sectionTag);
                    }
                }

                SaveHtmlFile(sections);
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
        public Sections GetById(int id, bool getChilds = false, bool getAnchors = false, bool getTags = false)
        {
            Sections section = sectionsDao.GetById(id);
            if (section == null)
            {
                return null;
            }
            if (getTags)
            {
                section.Tags = sectionTagDao.GetBySectionId(id);
            }
            if (getChilds)
            {
                section.Childs = sectionsDao.GetChilds(id);
            }
            if (getAnchors)
            {
                section.Anchors = anchorDao.GetBySectionId(id);
            }
            return section;
        }

        public Sections GetBySectionId(string sectionId, bool getChilds = false, bool getAnchors = false, bool getTags = false)
        {
            Sections section = sectionsDao.GetBySectionId(sectionId);
            if (section == null)
            {
                return null;
            }
            if (getChilds)
            {
                section.Childs = sectionsDao.GetChilds(section.Id);
            }
            if (getAnchors)
            {
                if (section.Childs != null && section.Childs.Count > 0)
                {
                    List<int> allSectionIds = section.Childs.Select(s => s.Id).ToList();
                    allSectionIds.Add(section.Id);

                    IList<SectionAnchors> allAnchors = anchorDao.GetBySectionIds(allSectionIds);
                    foreach (var child in section.Childs)
                    {
                        child.Anchors = allAnchors.Where(a => a.SectionId == child.Id).ToList();
                    }
                    section.Anchors = allAnchors.Where(a => a.SectionId == section.Id).ToList();
                }
                else
                {
                    section.Anchors = anchorDao.GetBySectionId(section.Id);
                }
            }
            if (getTags)
            {
                section.Tags = sectionTagDao.GetBySectionId(section.Id);
            }
            
            return section;
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        public IList<Sections> Query(SectionsQueryInfo queryInfo)
        {
            return sectionsDao.Query(queryInfo);
        }

        public IPagedList<Sections> QueryPaged(SectionsQueryInfo queryInfo)
        {
            return sectionsDao.QueryPaged(queryInfo);
        }

        #endregion

        #region 获取随笔段落列表（包括子段落)

        public IList<Sections> GetByBlogSectionIds(string blogSectionIds, bool getAnchors=false)
        {
            List<Sections> allSections = sectionsDao.GetByBlogSectionIds(blogSectionIds).ToList();
            if (getAnchors)
            {
                List<int> allSectionIds = allSections.Select(s => s.Id).ToList();
                IList<SectionAnchors> allAnchors = anchorDao.GetBySectionIds(allSectionIds);

                allSections.ForEach(s =>
                {
                    List<SectionAnchors> anchors = allAnchors.Where(a => a.SectionId == s.Id).ToList();
                    s.Anchors = anchors;
                });
            }
            return allSections;
        }

        #endregion

        #region Childs

        #region GetChilds

        public IList<Sections> GetChilds(IList<int> sectionIds)
        {
            return sectionsDao.GetChilds(sectionIds);
        }

        #endregion

        #region SaveChilds

        public void SaveChilds(Sections childs)
        {
            AssertUtils.Greater(childs.Id, 0);
            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                sectionsDao.DeleteChilds(childs.Id);
                List<int> childIds = new List<int>();
                //插件section
                if (childs.Childs != null)
                {
                    foreach (var child in childs.Childs)
                    {
                        SectionChilds item = new SectionChilds();
                        item.SectionId = childs.Id;
                        item.ChildId = child.Id;
                        sectionsDao.SaveChild(item);
                    }
                    childIds = childs.Childs.Select(s => s.Id).ToList();
                }
                sectionsDao.UpdateChildsInfo(childs.Id, childIds);
                unitOfWork.Complete();
            }
        }

        #endregion

        #endregion
    }
}