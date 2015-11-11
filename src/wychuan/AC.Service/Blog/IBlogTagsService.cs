using System.Collections.Generic;
using AC.Service.DTO.Blog;

namespace AC.Service.Blog
{
    public interface IBlogTagsService
    {
        int Create(BlogTagsDTO tag);

        IList<BlogTagsDTO> GetByUserId(int userId);
    }
}