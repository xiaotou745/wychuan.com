using System.Collections.Generic;
using AC.Page;
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
        #region  Create, Modify,Remove,Get,Query

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
        Sections GetById(int id, bool getChilds = false, bool getAnchors = false, bool getTags = false);

        /// <summary>
        /// 根据sectionId获取对象
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        Sections GetBySectionId(string sectionId, bool getChilds = false, bool getAnchors = false, bool getTags = false);

        /// <summary>
        /// 查询方法
        /// </summary>
        IList<Sections> Query(SectionsQueryInfo queryInfo);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        IPagedList<Sections> QueryPaged(SectionsQueryInfo queryInfo);

        #endregion

        #region 获取随笔的段落列表(包括子段落)

        /// <summary>
        /// 获取随笔的段落列表(包括子段落)
        /// </summary>
        /// <param name="blogSectionIds"></param>
        /// <param name="getAnchors">是否获取锚点，默认为false</param>
        /// <returns></returns>
        IList<Sections> GetByBlogSectionIds(string blogSectionIds,bool getAnchors=false);

        #endregion

        #region 获取子段落

        /// <summary>
        /// 获取子段落
        /// </summary>
        /// <param name="sectionIds"></param>
        /// <returns></returns>
        IList<Sections> GetChilds(IList<int> sectionIds);

        #endregion

        #region 更新子段落

        /// <summary>
        /// 保存子段落
        /// </summary>
        /// <param name="childs"></param>
        void SaveChilds(Sections childs);

        #endregion
    }
}