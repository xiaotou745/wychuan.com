using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.LiCai;

namespace AC.Service.LiCai
{
    public interface IBusinessService
    {
        int Create(BusinessDTO business);

        IList<BusinessDTO> GetByUserId(int userId);
    }
}
