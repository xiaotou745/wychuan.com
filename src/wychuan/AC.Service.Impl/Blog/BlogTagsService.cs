using System.Collections.Generic;
using AC.Dao.Blog;
using AC.Service.Blog;
using AC.Service.DTO.Blog;
using AC.Service.Impl.Cache;
using AC.Util;

namespace AC.Service.Impl.Blog
{
    public class BlogTagsService : IBlogTagsService
    {
        private readonly BlogTagsDao blogTagsDao;

        public BlogTagsService()
        {
            blogTagsDao = new BlogTagsDao();
        }

        public int Create(BlogTagsDTO tag)
        {
            int id = blogTagsDao.Insert(tag);
            BlogCacheProvider.RefreshBlogTagsInCache(tag.UserId);
            return id;
        }

        public IList<BlogTagsDTO> GetByUserId(int userId)
        {
            AssertUtils.Greater(userId, 0);
            return BlogCacheProvider.GetBlogTagsFromCache(userId);
        }
    }
}