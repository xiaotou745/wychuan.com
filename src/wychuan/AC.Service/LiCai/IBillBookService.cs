using System.Collections.Generic;
using AC.Service.DTO.LiCai;

namespace AC.Service.LiCai
{
    public interface IBillBookService
    {
        /// <summary>
        /// 新增一条记录
        ///<param name="book">要新增的对象</param>
        /// </summary>
        int Create(BillBooksDTO book);

        /// <summary>
        /// 修改一条记录
        /// </summary>
        void ModifyName(int id, string name);

        /// <summary>
        /// 删除一条记录
        /// </summary>
        void Remove(int iD);

        /// <summary>
        /// 根据用户ID获取账本列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<BillBooksDTO> GetByUserId(int userId);
    }
}
