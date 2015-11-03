using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.LiCai;

namespace AC.Service.LiCai
{
    public interface ILiCaiDetailService
    {
        int Create(LiCaiDetailsDTO licaiDetail);

        IList<LiCaiDetailsDTO> GetByUserId(int userId);

        void Remove(int id);
        LiCaiDetailsDTO GetById(int id);
        void Modify(LiCaiDetailsDTO detail);
    }
}
