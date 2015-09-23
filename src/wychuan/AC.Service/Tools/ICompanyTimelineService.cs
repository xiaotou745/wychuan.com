using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.Tools;

namespace AC.Service.Tools
{
    public interface ICompanyTimelineService
    {
        int Create(CompanyTimelineDTO timeline);
    }
}
