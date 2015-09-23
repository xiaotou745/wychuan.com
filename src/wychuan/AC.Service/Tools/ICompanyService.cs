using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.Tools;

namespace AC.Service.Tools
{
    public interface ICompanyService
    {
        int Create(CompanyDTO company);

        IList<CompanyDTO> GetByUserId(int userId);
    }
}
