using System;
using System.Collections.Generic;

namespace AC.Service.DTO.Blog
{
    /// <summary>
    /// 实体类BlogDTO 。(属性说明自动提取数据库字段的描述信息)
    /// Generate By: wangyuchuan
    /// Generate Time: 2015-12-16 11:20:38
    /// </summary>
    public class BlogsDTO
    {
        #region Fields

        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 字符串ID
        /// </summary>
        public string BlogId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 封面图片地址
        /// </summary>
        public string ConverImgUrl { get; set; }

        /// <summary>
        /// 二级分类ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 是否所有人可见
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string SectionIds { get; set; }

        /// <summary>
        /// 发布人Id
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PostTime { get; set; }

        #endregion

        public string CategoryName { get; set; }

        public int FirstCategoryId { get; set; }

        public string FirstCategoryName { get; set; }

        public IList<Sections> BlogSections { get; set; }

        /// <summary>
        /// 段落标签列表
        /// </summary>
        public IList<BlogTagRelation> Tags { get; set; }

        public string Htmls { get; set; }
    }

    public class BlogsQueryInfo
    {
        
    }
}