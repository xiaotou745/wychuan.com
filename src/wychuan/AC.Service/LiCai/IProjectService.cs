using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.LiCai;

namespace AC.Service.LiCai
{
    public interface IProjectService
    {
        void InitUserProject(int userId);

        int Create(ProjectDTO business);

        IList<ProjectDTO> GetByUserId(int userId);
    }
}
