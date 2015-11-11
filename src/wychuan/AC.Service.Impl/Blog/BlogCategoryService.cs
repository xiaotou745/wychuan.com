using System;
using System.Collections.Generic;
using AC.Dao.Blog;
using AC.Service.Blog;
using AC.Service.DTO.Blog;
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
            return blogCategoryDao.GetByUserId(userId);
        }

        public int Create(BlogCategoryDTO blogCategory)
        {
            AssertUtils.ArgumentNotNull(blogCategory, "blogCategory");
            AssertUtils.Greater(blogCategory.UserId, 0);

            return blogCategoryDao.Insert(blogCategory);
        }

        public void Rename(int id, string name)
        {
            AssertUtils.Greater(id, 0);
            AssertUtils.StringNotNullOrEmpty(name, "name");

            blogCategoryDao.Rename(id, name);
        }

        public void Remove(int id)
        {
            AssertUtils.Greater(id, 0);

            blogCategoryDao.Delete(id);
        }
    }
}