using System.Collections.Generic;
using AC.Page;
using AC.Service.DTO.Blog;

namespace wychuan2.com.Areas.admin.Models.Blog
{
    public class BlogListModel
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
        /// 随笔列表
        /// </summary>
        public IList<BlogsDTO> Blogs
        {
            get
            {
                if (BlogsPaged == null || BlogsPaged.ContentList == null)
                {
                    return new List<BlogsDTO>();
                }
                return BlogsPaged.ContentList;
            }
        }

        /// <summary>
        /// 分页随笔列表
        /// </summary>
        public IPagedList<BlogsDTO> BlogsPaged { get; set; }
    }
}