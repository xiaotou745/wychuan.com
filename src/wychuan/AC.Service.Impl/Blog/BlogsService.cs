using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class BlogsService : IBlogsService
    {
        private readonly BlogDao blogDao;
        private readonly BlogTagRelationDao blogTagDao;
        private readonly IBlogTagsService tagService;
        private readonly BlogSectionsDao blogSectionDao;
        private readonly ISectionsService sectionsService;

        public BlogsService()
        {
            blogDao = new BlogDao();
            tagService = new BlogTagsService();
            blogTagDao = new BlogTagRelationDao();
            blogSectionDao = new BlogSectionsDao();
            sectionsService = new SectionsService();
        }

        #region Create, Modify,Remove,Get,Query

        public int Create(BlogsDTO blog)
        {
            BlogsDTO existsBlog = blogDao.GetByBlogId(blog.BlogId);
            if (existsBlog != null)
            {
                throw new BlogIdExistsException();
            }
            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                int id = blogDao.Insert(blog);
                if (blog.Tags != null)
                {
                    foreach (BlogTagRelation tagItem in blog.Tags)
                    {
                        tagItem.BlogId = id;
                        if (tagItem.TagId == 0) //tags不存在，先创建tag，然后再插入section tags.
                        {
                            BlogTagsDTO tagInfo = new BlogTagsDTO {UserId = blog.AuthorId, TagName = tagItem.TagName};
                            tagItem.TagId = tagService.Create(tagInfo);
                        }
                        blogTagDao.Insert(tagItem);
                    }
                }
                unitOfWork.Complete();
                return id;
            }
        }

        public void Modify(BlogsDTO blog)
        {
            BlogsDTO existsBlog = blogDao.GetByBlogId(blog.BlogId);
            if (existsBlog != null && existsBlog.Id != blog.Id)
            {
                throw new BlogIdExistsException();
            }
            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                blogDao.Update(blog);
                blogTagDao.DeleteByBlogId(blog.Id);
                if (blog.Tags != null)
                {
                    foreach (BlogTagRelation tagItem in blog.Tags)
                    {
                        tagItem.BlogId = blog.Id;
                        if (tagItem.TagId == 0) //tags不存在，先创建tag，然后再插入section tags.
                        {
                            BlogTagsDTO tagInfo = new BlogTagsDTO {UserId = blog.AuthorId, TagName = tagItem.TagName};
                            tagItem.TagId = tagService.Create(tagInfo);
                        }
                        blogTagDao.Insert(tagItem);
                    }
                }
                unitOfWork.Complete();
            }
        }


        public void Remove(int id)
        {
            blogDao.Delete(id);
        }

        public BlogsDTO GetById(int id)
        {
            BlogsDTO blogsDTO = blogDao.GetById(id);
            if (blogsDTO == null)
            {
                return null;
            }
            blogsDTO.Tags = blogTagDao.GetByBlogId(id);
            return blogsDTO;
        }

        public IList<BlogsDTO> Query(BlogsQueryInfo queryInfo)
        {
            return blogDao.Query(queryInfo);
        }

        public IPagedList<BlogsDTO> QueryPaged(BlogsQueryInfo queryInfo)
        {
            return blogDao.QueryPaged(queryInfo);
        }

        public void SaveHtmlFile(BlogsDTO blog)
        {
            string basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string savePath = Path.Combine(basePath, "blogs", blog.BlogId, blog.BlogId + ".html");
            FilesHelper.CreateFileDir(savePath);
            FileIO.SaveTextFile(savePath, blog.Htmls, System.Text.Encoding.UTF8);
        }

        #endregion

        #region ModifySections

        public void ModifySections(BlogsDTO blog)
        {
            using (IUnitOfWork unitOfWork = UnitOfWorkFactoryProvider.GetUnitOfWork())
            {
                blogDao.UpdateSectionIds(blog.Id, blog.SectionIds);

                blogSectionDao.DeleteByBlogId(blog.Id);

                if (blog.BlogSections != null)
                {
                    foreach (var bs in blog.BlogSections)
                    {
                        blogSectionDao.Insert(new BlogSections {BlogId = blog.Id, SectionId = bs.Id});
                    }
                }

                BlogsDTO blogsDTO = blogDao.GetById(blog.Id);
                blogsDTO.Htmls = blog.Htmls;
                SaveHtmlFile(blogsDTO);
                unitOfWork.Complete();
            }
        }

        #endregion

        public BlogsDTO GetByIdWithSections(int id)
        {
            BlogsDTO blogsDTO = blogDao.GetById(id);
            if (blogsDTO == null)
            {
                return null;
            }
            blogsDTO.BlogSections = GetBlogSections(blogsDTO.SectionIds);

            return blogsDTO;
        }

        public BlogsDTO GetByBlogIdWithSections(string blogId)
        {
            BlogsDTO blogsDTO = blogDao.GetByBlogId(blogId);
            if (blogsDTO == null)
            {
                return null;
            }
            blogsDTO.BlogSections = GetBlogSections(blogsDTO.SectionIds);

            return blogsDTO;
        }

        public BlogsDTO GetByIdWithSections(int id, string sectionIds)
        {
            BlogsDTO blogsDTO = blogDao.GetById(id);
            if (blogsDTO == null)
            {
                return null;
            }
            blogsDTO.BlogSections = GetBlogSections(sectionIds);

            return blogsDTO;
        }

        /// <summary>
        /// 获取随笔的段落
        /// </summary>
        /// <param name="sectionIds"></param>
        /// <returns></returns>
        private IList<Sections> GetBlogSections(string sectionIds)
        {
            if (!string.IsNullOrEmpty(sectionIds))
            {
                //随笔的段落及子段落
                IList<Sections> allSecions = sectionsService.GetByBlogSectionIds(sectionIds, true);
                
                //随笔段落
                List<Sections> rootSections = allSecions.Where(s => s.ParentId==0).ToList();

                //每个段落的子段落设置
                rootSections.ForEach(s =>
                {
                    List<Sections> lstChilds = allSecions.Where(child => child.ParentId == s.Id).ToList();
                    s.Childs = lstChilds;
                });
                return rootSections;
            }
            else
            {
                return new List<Sections>();
            }
        }
    }
}