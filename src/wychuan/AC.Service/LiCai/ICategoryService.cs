using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.LiCai;

namespace AC.Service.LiCai
{
    public interface ICategoryService
    {
        int Create(CategoryDTO category);

        void Rename(int id, string newName);

        void InitUser(int userId);

        IList<CategoryDTO> GetByUserId(int userId);
        void Remove(int id);
    }
}
