using System.Collections.Generic;

namespace AC.Web.Common.Config
{
    /// <summary>
    /// 分类Service
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <returns></returns>
        IList<CategoryConfigInfo> GetAll();

        /// <summary>
        /// 创建一个新分类
        /// </summary>
        /// <param name="category">要创建的分类对象</param>
        /// <returns>返回新创建的分类Id</returns>
        int Create(CategoryConfigInfo category);
    }
}