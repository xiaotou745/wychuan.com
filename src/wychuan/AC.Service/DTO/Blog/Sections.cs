using System;
using System.Collections.Generic;

namespace AC.Service.DTO.Blog
{
    public class Sections
    {
        /// <summary>
        /// section对应的tagId
        /// </summary>
        public List<int> TagIds { get; set; }

        /// <summary>
        /// 创建section时新的tag名称列表，中间以逗号分隔
        /// </summary>
        public string NewTags { get; set; }

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
        public int ParentId { get; set; }
    }
}