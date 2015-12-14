using System.Collections.Generic;
using AC.Service.DTO.Blog;

namespace wychuan2.com.Areas.admin.Models.Blog
{
    /// <summary>
    /// 段落列表视图模型对象
    /// </summary>
    public class SectionListModel
    {
        public IList<Sections> Sections { get; set; }
    }
}