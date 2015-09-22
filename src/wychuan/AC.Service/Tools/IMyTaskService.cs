using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.Tools;

namespace AC.Service.Tools
{
    public interface IMyTaskService
    {
        int Create(MyTaskDTO myTask);

        void Modify(MyTaskDTO myTask);

        IList<MyTaskDTO> GetMyTasks(MyTaskQueryInfo queryInfo);

        void ModifyStatus(int id, int status);

        void Hide(int id);

        void Remove(int id);
    }
}
