using AC.Service.DTO.Blog;

namespace AC.Service.Blog
{
    public interface IBlogsService
    {
        /// <summary>
        /// 新增一条记录
        ///<param name="blog">要新增的对象</param>
        /// </summary>
        int Create(BlogsDTO blog);

        /// <summary>
        /// 修改一条记录
        ///<param name="blog">要修改的对象</param>
        /// </summary>
        void Modify(BlogsDTO blog);

        /// <summary>
        /// 删除一条记录
        /// </summary>
        void Remove(int id);

        /// <summary>
        /// 根据Id得到一个对象实体
        /// </summary>
        BlogsDTO GetById(int id);
    }
}