using System;
using System.Collections.Generic;
using AC.Extension;

namespace AC.Service.DTO.Blog
{
    public class Sections
    {
        /// <summary>
        /// 段落标签列表
        /// </summary>
        public IList<SectionsTags> Tags { get; set; }

        /// <summary>
        /// 二级段落
        /// </summary>
        public IList<Sections> Childs { get; set; }

        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 段落ID
        /// </summary>
        public string SectionId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// html内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 二级目录ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 二级目录名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 一级目录ID
        /// </summary>
        public int FirstCategoryId { get;set; }

        /// <summary>
        /// 一级目录名称
        /// </summary>
        public string FirstCategoryName { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }


    /// <summary>
    /// 查询对象类RequestBase 。(属性说明自动提取数据库字段的描述信息)
    /// Generate By: wangyuchuan
    /// Generate Time: 2015-12-11 17:39:47
    /// </summary>
    public class SectionsQueryInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// sectionId
        /// </summary>
        public string SectionId { get; set; }

        /// <summary>
        /// parentId
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// sectionId列表查询条件
        /// </summary>
        public List<string> SectionIds { get; set; }

        /// <summary>
        /// 标签列表查询条件
        /// </summary>
        public List<int> TagIds { get; set; }

        public string StrTagIds { get; set; }

        /// <summary>
        /// 当前随笔已选择的段落ID列表
        /// </summary>
        public List<int> CheckedSectionIds {
            get
            {
                if (string.IsNullOrEmpty(StrCheckedSectionIds))
                {
                    return new List<int>();
                }
                return StrCheckedSectionIds.ToNumberList();
            } 
        }

        /// <summary>
        /// 已选
        /// </summary>
        public string StrCheckedSectionIds { get; set; }
    }
}