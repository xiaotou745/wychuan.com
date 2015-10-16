using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.Tools;

namespace AC.Service.Tools
{
    public interface IMyNotesService
    {
        /// <summary>
        /// 新增一条记录
        ///<param name="myNotesDTO">要新增的对象</param>
        /// </summary>
        int Create(MyNotesDTO myNotesDTO);

        IList<MyNotesDTO> Query(MyNotesQueryInfo queryInfo);
        void Remove(int id);
    }
}