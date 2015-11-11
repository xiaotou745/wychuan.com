using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.Blog;

namespace AC.Service.Blog
{
    public interface IBlogCategoryService
    {
        /// <summary>
        /// 获取指定用户的随笔分类
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<BlogCategoryDTO> GetByUserId(int userId);

        /// <summary>
        /// 创建随笔分类
        /// </summary>
        /// <param name="blogCategory"></param>
        /// <returns></returns>
        int Create(BlogCategoryDTO blogCategory);

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        void Rename(int id, string name);

        /// <summary>
        /// 删除一个类目
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);
    }
}
