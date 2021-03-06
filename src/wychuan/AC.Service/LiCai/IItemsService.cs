﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.LiCai;

namespace AC.Service.LiCai
{
    public interface IItemsService
    {
        void InitUserItems(int userId);

        int Create(ItemDTO item);

        void Rename(int id, string name);

        void Remove(int id);

        IList<ItemDTO> GetByUserId(int userId);

        IList<ItemDTO> GetByUserId(int userId, ItemType type);
    }
}
