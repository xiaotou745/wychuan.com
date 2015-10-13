using System;
using System.Collections.Generic;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
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

            return categoryDao.Insert(category);
        }

        public void Rename(int id, string newName)
        {
            AssertUtils.Greater(id, 0);
            AssertUtils.ArgumentNotNull(newName, "name");

            categoryDao.Rename(id, newName);
        }

        public void InitUser(int userId)
        {
            AssertUtils.Greater(userId, 0);

            categoryDao.InitUser(userId);
        }

        public IList<CategoryDTO> GetByUserId(int userId)
        {
            return categoryDao.GetByUserId(userId);
        }

        public void Remove(int id)
        {
            AssertUtils.Greater(id, 0);

            categoryDao.Delete(id);
        }
    }
}