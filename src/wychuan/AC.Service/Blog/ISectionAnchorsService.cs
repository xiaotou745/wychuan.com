using System.Collections.Generic;
using AC.Service.DTO.Blog;

namespace AC.Service.Blog
{
    public interface ISectionAnchorsService
    {
        /// <summary>
        /// 新增一条记录
        ///<param name="sectionAnchors">要新增的对象</param>
        /// </summary>
        int Create(SectionAnchors sectionAnchors);

        /// <summary>
        /// 修改一条记录
        ///<param name="sectionAnchors">要修改的对象</param>
        /// </summary>
        void Modify(SectionAnchors sectionAnchors);

        /// <summary>
        /// 删除一条记录
        /// </summary>
        void Remove(int id);

        /// <summary>
        /// 获取指定段落的锚点列表
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        IList<SectionAnchors> GetBySectionId(int sectionId);

        IList<SectionAnchors> GetBySectionIds(IList<int> sectionIds);
    }
}