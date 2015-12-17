using System.Collections.Generic;
using AC.Page;
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

        void ModifySections(BlogsDTO blog);

        /// <summary>
        /// 删除一条记录
        /// </summary>
        void Remove(int id);

        /// <summary>
        /// 根据Id得到一个对象实体
        /// </summary>
        BlogsDTO GetById(int id);

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="queryInfo">查询条件</param>
        /// <returns></returns>
        IList<BlogsDTO> Query(BlogsQueryInfo queryInfo);

        /// <summary>
        /// 分页查询方法
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        IPagedList<BlogsDTO> QueryPaged(BlogsQueryInfo queryInfo);

        BlogsDTO GetByIdWithSections(int id);

        BlogsDTO GetByBlogIdWithSections(string blogId);

        BlogsDTO GetByIdWithSections(int id, string sectionIds);
    }
}