using System;
using System.Collections.Generic;
using System.Linq;
using AC.Extension;
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
        /// 当前要编辑的段落对象
        /// </summary>
        public Sections CurrentSection { get; set; }

        /// <summary>
        /// 父段落
        /// </summary>
        public Sections ParentSection { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public SectionOperate Operate { get; set; }

        #region 快捷属性
        /// <summary>
        /// 当前要编辑的段落ID
        /// </summary>
        public int CurrentId
        {
            get
            {
                if (IsModify)
                {
                    return CurrentSection.Id;
                }
                return 0;
            }
        }

        public string CurrentTitle
        {
            get
            {
                if (IsModify)
                {
                    return CurrentSection.Title;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 当前要添加的段落父ID
        /// </summary>
        public int ParentId
        {
            get
            {
                if (IsModify)
                {
                    return CurrentSection.ParentId;
                }
                if (Operate.Equals(SectionOperate.CreateChild))
                {
                    return ParentSection.Id;
                }
                return 0;
            }
        }

        public string CurrentContext
        {
            get
            {
                if (IsModify)
                {
                    return CurrentSection.Content;
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 一级类目选中项
        /// </summary>
        public int FirstCategoryIdSelected
        {
            get
            {
                if (Operate.Equals(SectionOperate.CreateChild))//如果是创建子段落，则一级类目与父段落相同
                {
                    return ParentSection.FirstCategoryId;
                }
                //不论是编辑子还是父段落，都是取当前段落的
                else if (Operate.Equals(SectionOperate.ModifyChild) || Operate.Equals(SectionOperate.ModifyParent))
                {
                    return CurrentSection.FirstCategoryId;
                }
                return 0;
            }
        }
        /// <summary>
        /// 获取当前选择的二级类目
        /// </summary>
        public int CategoryIdSelected
        {
            get
            {
                if (Operate.Equals(SectionOperate.CreateChild))//如果是创建子段落，则一级类目与父段落相同
                {
                    return ParentSection.CategoryId;
                }
                //不论是编辑子还是父段落，都是取当前段落的
                else if (Operate.Equals(SectionOperate.ModifyChild) || Operate.Equals(SectionOperate.ModifyParent))
                {
                    return CurrentSection.CategoryId;
                }
                return 0;
            }
        }

        /// <summary>
        /// 是否显示类目和标签输入项
        /// 只有父菜单才需要显示，否则不显示
        /// </summary>
        public bool ShowCategoryAndTags
        {
            get
            {
                return IsParent;
            }
        }

        /// <summary>
        /// 是否是父段落
        /// </summary>
        public bool IsParent
        {
            get
            {
                return Operate.Equals(SectionOperate.CreateNew) || Operate.Equals(SectionOperate.ModifyParent);
            }
        }

        private bool IsModify
        {
            get { return Operate.Equals(SectionOperate.ModifyParent) || Operate.Equals(SectionOperate.ModifyChild); }
        }

        public string SectionIdShow
        {
            get
            {
                if (IsModify)
                {
                    return CurrentSection.SectionId;
                }
                if (Operate.Equals(SectionOperate.CreateChild))
                {
                    return ParentSection.SectionId;
                }
                return string.Empty;
            }
        }

        public string ParentText
        {
            get
            {
                if (IsParent)
                {
                    return "无";
                }
                if (Operate.Equals(SectionOperate.CreateChild))
                {
                    return ParentSection.Title + "(" + ParentSection.SectionId + ")";
                }
                return "";
            }
        }

        public string TagText
        {
            get
            {
                if (IsModify)
                {
                    return CurrentSection.Tags.Select(t => t.TagName).ToList().SplitString(',');
                }
                if (Operate.Equals(SectionOperate.CreateChild))
                {
                    return ParentSection.Tags.Select(t => t.TagName).ToList().SplitString(',');
                }
                return string.Empty;
            }
        }
        #endregion
    }

    public enum SectionOperate
    {
        /// <summary>
        /// 创建新段落
        /// </summary>
        CreateNew = 0,

        /// <summary>
        /// 创建新子段落
        /// </summary>
        CreateChild = 1,

        /// <summary>
        /// 编辑父段落
        /// </summary>
        ModifyParent = 2,

        /// <summary>
        /// 编辑子段落
        /// </summary>
        ModifyChild = 3,
    }
}