using AC.Dao.Blog;
using AC.Service.Blog;
using AC.Service.DTO.Blog;

namespace AC.Service.Impl.Blog
{
    public class BlogsService : IBlogsService
    {
        private readonly BlogDao blogDao;

        public BlogsService()
        {
            blogDao = new BlogDao();
        }

        public int Create(BlogsDTO blog)
        {
            return blogDao.Insert(blog);
        }

        public void Modify(BlogsDTO blog)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public BlogsDTO GetById(int id)
        {
            return blogDao.GetById(id);
        }
    }
}