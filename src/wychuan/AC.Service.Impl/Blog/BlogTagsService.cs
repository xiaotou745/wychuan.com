using System.Collections.Generic;
using AC.Dao.Blog;
using AC.Service.Blog;
using AC.Service.DTO.Blog;

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
            return blogTagsDao.Insert(tag);
        }

        public IList<BlogTagsDTO> GetByUserId(int userId)
        {
            return blogTagsDao.GetByUserId(userId);
        }
    }
}