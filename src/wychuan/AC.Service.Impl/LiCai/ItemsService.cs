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
    public class ItemsService : IItemsService
    {
        private readonly ItemsDao itemsDao;

        public ItemsService()
        {
            itemsDao = new ItemsDao();
        }
        public void InitUserItems(int userId)
        {
            AssertUtils.Greater(userId, 0);

            itemsDao.InitUserItems(userId);
        }

        public int Create(ItemDTO item)
        {
            AssertUtils.ArgumentNotNull(item, "item");
            int id = itemsDao.Insert(item);

            LiCaiCacheProvider.RefreshItemsInCache(item.UserId);

            return id;
        }

        public void Rename(int id, string name)
        {
            AssertUtils.Greater(id, 0);
            AssertUtils.ArgumentNotNull(name, "name");

            itemsDao.Rename(id, name);
            ItemDTO itemDTO = itemsDao.GetById(id);
            LiCaiCacheProvider.RefreshItemsInCache(itemDTO.UserId);
        }

        public void Remove(int id)
        {
            AssertUtils.Greater(id, 0);

            ItemDTO itemDTO = itemsDao.GetById(id);

            itemsDao.Delete(id);

            LiCaiCacheProvider.RefreshItemsInCache(itemDTO.UserId);
        }

        public IList<ItemDTO> GetByUserId(int userId)
        {
            return LiCaiCacheProvider.GetItemsInCache(userId);
            //return itemsDao.GetByUserId(userId);
        }

        public IList<ItemDTO> GetByUserId(int userId, ItemType type)
        {
            IList<ItemDTO> allItems = GetByUserId(userId);
            return allItems.Where(i => i.Type.Equals(type)).ToList();
            //return itemsDao.GetByUserId(userId, type);
        }
    }
}