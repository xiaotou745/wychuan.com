using System.Collections.Generic;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
using AC.Service.LiCai;
using AC.Util;

namespace AC.Service.Impl.LiCai
{
    public class BillBookService : IBillBookService
    {
        private readonly BillBookDao bookDao;

        public BillBookService()
        {
            this.bookDao = new BillBookDao();
        }

        public void InitUserBooks(int userId)
        {
            bookDao.InitUserBooks(userId);
        }

        public int Create(BillBooksDTO book)
        {
            AssertUtils.ArgumentNotNull(book, "book");

            return bookDao.Insert(book);
        }

        public void ModifyName(int id,string name)
        {
            bookDao.Update(id, name);
        }

        public void Remove(int id)
        {
            bookDao.Delete(id);
        }

        public IList<BillBooksDTO> GetByUserId(int userId)
        {
            return bookDao.GetByUserId(userId);
        }
    }
}