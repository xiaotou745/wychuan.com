using System.Collections.Generic;
using AC.Service.DTO.Blog;

namespace wychuan2.com.Areas.admin.Models.Blog
{
    /// <summary>
    /// 段落发布视图模型对象
    /// </summary>
    public class SectionPublishModel
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
        /// 当前要添加的段落父ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 当前要编辑的段落ID
        /// </summary>
        public int CurrentId { get; set; }

        /// <summary>
        /// 当前要编辑的段落对象
        /// </summary>
        public Sections CurrentSection { get; set; }

        /// <summary>
        /// 父段落
        /// </summary>
        public Sections ParentSection { get; set; }
    }
}