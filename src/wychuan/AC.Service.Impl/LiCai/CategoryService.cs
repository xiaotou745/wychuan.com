using System;
using System.Collections.Generic;
using System.Linq;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
using AC.Service.Impl.Cache;
using AC.Service.LiCai;
using AC.Util;

namespace AC.Service.Impl.LiCai
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryDao categoryDao;

        public CategoryService()
        {
            categoryDao = new CategoryDao();
        }

        public int Create(CategoryDTO category)
        {
            AssertUtils.ArgumentNotNull(category, "category");

            int id = categoryDao.Insert(category);

            LiCaiCacheProvider.RefreshCategoriesInCache(category.UserId);

            return id;
        }

        public void Rename(int id, string newName)
        {
            AssertUtils.Greater(id, 0);
            AssertUtils.ArgumentNotNull(newName, "name");

            categoryDao.Rename(id, newName);

            CategoryDTO currentCategory = categoryDao.GetById(id);
            LiCaiCacheProvider.RefreshCategoriesInCache(currentCategory.UserId);
        }

        public void InitUser(int userId)
        {
            AssertUtils.Greater(userId, 0);

            categoryDao.InitUser(userId);
        }

        public IList<CategoryDTO> GetByUserId(int userId)
        {
            return LiCaiCacheProvider.GetCategoriesInCache(userId);
        }

        public CategoryDTO GetByName(int userId, int type, string name)
        {
            IList<CategoryDTO> allCategories = LiCaiCacheProvider.GetCategoriesInCache(userId);
            return
                allCategories.FirstOrDefault(
                    c => c.UserId == userId && c.InOutType.GetHashCode() == type && c.Name == name);
        }

        public void Remove(int id)
        {
            AssertUtils.Greater(id, 0);

            CategoryDTO categoryDTO = categoryDao.GetById(id);

            categoryDao.Delete(id);

            LiCaiCacheProvider.RefreshCategoriesInCache(categoryDTO.UserId);
        }
    }
}