using System.Collections.Generic;
using System.Linq;
using AC.Extension;
using AC.Service.DTO.Blog;

namespace wychuan2.com.Areas.admin.Models.Blog
{
    public class BlogModel
    {
        /// <summary>
        /// 所有的类目
        /// </summary>
        public IList<BlogCategoryDTO> Categories { get; set; }

        /// <summary>
        /// 所有的tags
        /// </summary>
        public IList<BlogTagsDTO> Tags { get; set; }

        /// <summary>
        /// 当前blog
        /// </summary>
        public BlogsDTO CurrentBlogs { get; set; }

        public bool IsPublic
        {
            get
            {
                if (CurrentBlogs == null)
                {
                    return true;
                }
                return CurrentBlogs.IsPublic;
            }
        }

        public int FirstCategoryId
        {
            get
            {
                return CurrentBlogs == null ? 0 : CurrentBlogs.FirstCategoryId;
            }
        }

        public int CategoryId
        {
            get
            {
                return CurrentBlogs == null ? 0 : CurrentBlogs.CategoryId;
            }
        }

        public string TagText
        {
            get
            {
                if (CurrentBlogs == null)
                {
                    return string.Empty;
                }
                return CurrentBlogs.Tags.Select(t => t.TagName).ToList().SplitString(',');
            }
        }

        /// <summary>
        /// 待选择的段落
        /// </summary>
        public IList<Sections> SectionsUnChecked { get; set; }
    }

    public class BlogPreModel
    {
        public int Id { get; set; }

        public string SectionIds { get; set; }
    }
}