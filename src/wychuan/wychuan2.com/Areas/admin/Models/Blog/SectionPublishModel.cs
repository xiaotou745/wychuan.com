using System;
using System.Collections.Generic;
using System.Linq;
using AC.Extension;
using AC.Service.DTO.Blog;

namespace wychuan2.com.Areas.admin.Models.Blog
{
    public class SectionChildsModel
    {
        /// <summary>
        /// 基础数据-已有的tags
        /// </summary>
        public IList<BlogTagsDTO> Tags { get; set; }

        /// <summary>
        /// 当前段落
        /// </summary>
        public Sections CurrentSection { get; set; }

        /// <summary>
        /// 待选段落列表
        /// </summary>
        public IList<Sections> UnCheckedSections { get; set; }
    }
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
        /// 当前要编辑的段落对象
        /// </summary>
        public Sections CurrentSection { get; set; }

        #region 快捷属性
        /// <summary>
        /// 当前要编辑的段落ID
        /// </summary>
        public int CurrentId
        {
            get
            {
                return CurrentSection == null ? 0 : CurrentSection.Id;
            }
        }
        public string CurrentContent
        {
            get
            {
                return CurrentSection == null ? string.Empty : CurrentSection.Content;
            }
        }
        public string CurrentTitle
        {
            get
            {
                return CurrentSection == null ? string.Empty : CurrentSection.Title;
            }
        }

        public string SectionId
        {
            get
            {
                return CurrentSection == null ? string.Empty : CurrentSection.SectionId;
            }
        }

        /// <summary>
        /// 一级类目选中项
        /// </summary>
        public int FirstCategoryIdSelected
        {
            get
            {
                if (CategoryIdSelected == 0)
                {
                    return 0;
                }
                BlogCategoryDTO parentCategory = Categories.FirstOrDefault(c => c.Id == CategoryIdSelected);
                return parentCategory == null ? 0 : parentCategory.ParentId;
            }
        }
        /// <summary>
        /// 获取当前选择的二级类目
        /// </summary>
        public int CategoryIdSelected
        {
            get
            {
                return CurrentSection == null ? 0 : CurrentSection.CategoryId;
            }
        }

        public string TagText
        {
            get
            {
                return CurrentSection == null
                    ? string.Empty
                    : CurrentSection.Tags.Select(t => t.TagName).ToList().SplitString(',');
            }
        }
        #endregion
    }
}