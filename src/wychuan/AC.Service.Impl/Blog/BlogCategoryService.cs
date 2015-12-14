using System;
using System.Collections.Generic;
using AC.Dao.Blog;
using AC.Service.Blog;
using AC.Service.DTO.Blog;
using AC.Service.Impl.Cache;
using AC.Util;

namespace AC.Service.Impl.Blog
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly BlogCategoryDao blogCategoryDao;

        public BlogCategoryService()
        {
            blogCategoryDao = new BlogCategoryDao();
        }

        public IList<BlogCategoryDTO> GetByUserId(int userId)
        {
            AssertUtils.Greater(userId, 0);
            return BlogCacheProvider.GetBlogCategoriesFromCache(userId);
        }

        public int Create(BlogCategoryDTO blogCategory)
        {
            AssertUtils.ArgumentNotNull(blogCategory, "blogCategory");
            AssertUtils.Greater(blogCategory.UserId, 0);

            int id = blogCategoryDao.Insert(blogCategory);
            BlogCacheProvider.RefreshBlogCategoriesInCache(blogCategory.UserId);
            return id;
        }

        public void Rename(int id, string name)
        {
            AssertUtils.Greater(id, 0);
            AssertUtils.StringNotNullOrEmpty(name, "name");

            BlogCategoryDTO blogCategoryDTO = blogCategoryDao.GetById(id);
            AssertUtils.ArgumentNotNull(blogCategoryDTO, "此category不存在");

            blogCategoryDao.Rename(id, name);
            BlogCacheProvider.RefreshBlogCategoriesInCache(blogCategoryDTO.UserId);
        }

        public void Remove(int id)
        {
            AssertUtils.Greater(id, 0);
            BlogCategoryDTO blogCategoryDTO = blogCategoryDao.GetById(id);
            AssertUtils.ArgumentNotNull(blogCategoryDTO, "此category不存在");

            blogCategoryDao.Delete(id);
            BlogCacheProvider.RefreshBlogCategoriesInCache(blogCategoryDTO.UserId);
        }
    }
}