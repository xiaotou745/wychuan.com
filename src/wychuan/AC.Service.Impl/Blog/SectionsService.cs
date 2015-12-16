﻿using System.Collections.Generic;
using System.IO;
using System.Web;
using AC.Dao;
using AC.Dao.Blog;
using AC.IO;
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
                if (sections.Tags != null)
                {
                    foreach (SectionsTags sectionTag in sections.Tags)
                    {
                        sectionTag.SectionId = id;
                        if (sectionTag.TagId == 0)//tags不存在，先创建tag，然后再插入section tags.
                        {
                            BlogTagsDTO tagInfo = new BlogTagsDTO { UserId = sections.UserId, TagName = sectionTag.TagName };
                            sectionTag.TagId =  tagDao.Create(tagInfo);
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
                        if (sectionTag.TagId == 0)//tags不存在，先创建tag，然后再插入section tags.
                        {
                            BlogTagsDTO tagInfo = new BlogTagsDTO { UserId = sections.UserId, TagName = sectionTag.TagName };
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

        public IList<Sections> GetByBlogSectionIds(string blogSectionIds)
        {
            return sectionsDao.GetByBlogSectionIds(blogSectionIds);
        }
    }
}