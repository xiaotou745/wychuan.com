using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AC.Web.Common.Config
{
    /// <summary>
    /// 类目管理Service
    /// </summary>
    public class CategoryConfigHelper : ConfigBase, ICategoryService
    {
        public CategoryConfigHelper()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                if (HttpContext.Current != null)
                {
                    FilePath = HttpContext.Current.Server.MapPath("~/config/categories.config");
                }
                else
                {
                    FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config\categories.config");
                }
            }
        }

        public IList<CategoryConfigInfo> GetAll()
        {
            return Get<List<CategoryConfigInfo>>() ?? new List<CategoryConfigInfo>();
        }

        public int Create(CategoryConfigInfo category)
        {
            var lstAll = GetAll();
            if (category.ParentId == 0)
            {
                lstAll.Add(category);
            }
            else
            {
                var parentCategory = lstAll.FirstOrDefault(c => c.Id == category.ParentId);
                if (parentCategory != null)
                {
                    parentCategory.Childs.Add(category);
                }
            }
            Save(lstAll);
            return category.Id;
        }


        public void Remove(int id, int parentId)
        {
            var lstAll = GetAll();
            if (lstAll == null || lstAll.Count == 0)
            {
                return;
            }
            CategoryConfigInfo removedItem;
            if (parentId == 0)
            {
                removedItem = lstAll.FirstOrDefault(c => c.Id == id);
                lstAll.Remove(removedItem);
            }
            else
            {
                var removedItemParent = lstAll.FirstOrDefault(c => c.Id == parentId);
                removedItem = removedItemParent.Childs.FirstOrDefault(c => c.Id == id);
                removedItemParent.Childs.Remove(removedItem);
            }
            
            Save(lstAll);
        }


        public void Modify(CategoryConfigInfo modifyCategory)
        {
            var lstAll = GetAll();
            if (lstAll == null || lstAll.Count == 0)
            {
                return;
            }
            CategoryConfigInfo modifyItem;
            if (modifyCategory.ParentId == 0)
            {
                modifyItem = lstAll.FirstOrDefault(c => c.Id == modifyCategory.Id);
            }
            else
            {
                var removedItemParent = lstAll.FirstOrDefault(c => c.Id == modifyCategory.ParentId);
                modifyItem = removedItemParent.Childs.FirstOrDefault(c => c.Id == modifyCategory.Id);
            }
            if (modifyItem != null)
            {
                modifyItem.Name = modifyCategory.Name;
                modifyItem.ParentId = modifyCategory.ParentId;
                Save(lstAll);
            }
        }
    }

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