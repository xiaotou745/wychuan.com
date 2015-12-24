using System.Collections.Generic;
using AC.Page;
using AC.Service.DTO.Blog;

namespace wychuan2.com.Areas.admin.Models.Blog
{
    /// <summary>
    /// 段落列表视图模型对象
    /// </summary>
    public class SectionListModel
    {
        /// <summary>
        /// 基础数据-已有的分类
        /// </summary>
        public IList<BlogCategoryDTO> Categories { get; set; }

        /// <summary>
        /// 基础数据-已有的tags
        /// </summary>
        public IList<BlogTagsDTO> Tags { get; set; }

        /// <summary>
        /// 分页查询结果
        /// </summary>
        public IPagedList<Sections> PagedSections { get; set; }

        public IList<Sections> Sections { get; set; }

        /// <summary>
        /// 父段落列表
        /// </summary>
        public bool IsParents { get; set; }
    }
}