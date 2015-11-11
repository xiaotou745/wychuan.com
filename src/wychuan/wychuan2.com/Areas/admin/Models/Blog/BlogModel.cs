using System.Collections.Generic;
using AC.Service.DTO.Blog;

namespace wychuan2.com.Areas.admin.Models.Blog
{
    public class BlogModel
    {
        public IList<BlogCategoryDTO> Categories { get; set; }

        public IList<BlogTagsDTO> Tags { get; set; } 
    }
}