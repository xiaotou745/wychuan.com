using System.Collections.Generic;

namespace AC.Service.Config
{
    /// <summary>
    /// 分类对象
    /// </summary>
    public class CategoryConfigInfo
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父分类Id
        /// </summary>
        public int ParentId { get; set; }

        private List<CategoryConfigInfo> childs = new List<CategoryConfigInfo>();

        /// <summary>
        /// 子分类列表
        /// </summary>
        public List<CategoryConfigInfo> Childs
        {
            get { return childs; }
            set { childs = value; }
        }
    }
}