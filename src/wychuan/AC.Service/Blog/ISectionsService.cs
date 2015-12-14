using System.Collections.Generic;
using AC.Service.DTO.Blog;

namespace AC.Service.Blog
{
    /// <summary>
    /// 业务逻辑类ISectionsService 的摘要说明。
    /// Generate By: mywww2.wychuan.com
    /// Generate Time: 2015-12-11 17:39:47
    /// </summary>
    public interface ISectionsService
    {
        /// <summary>
        /// 新增一条记录
        ///<param name="sections">要新增的对象</param>
        /// </summary>
        int Create(Sections sections);

        /// <summary>
        /// 修改一条记录
        ///<param name="sections">要修改的对象</param>
        /// </summary>
        void Modify(Sections sections);

        /// <summary>
        /// 删除一条记录
        /// </summary>
        void Remove(int id);

        /// <summary>
        /// 根据Id得到一个对象实体
        /// </summary>
        Sections GetById(int id);

        /// <summary>
        /// 根据sectionId获取对象
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        Sections GetBySectionId(string sectionId);

        /// <summary>
        /// 查询方法
        /// </summary>
        IList<Sections> Query(SectionsQueryInfo queryInfo);
    }
}