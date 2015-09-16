using System;
using System.Collections.Generic;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
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

            return itemsDao.Insert(item);
        }

        public IList<ItemDTO> GetByUserId(int userId)
        {
            return itemsDao.GetByUserId(userId);
        }

        public IList<ItemDTO> GetByUserId(int userId, ItemType type)
        {
            return itemsDao.GetByUserId(userId, type);
        }
    }
}