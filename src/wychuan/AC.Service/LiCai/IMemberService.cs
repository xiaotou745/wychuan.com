using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.LiCai;

namespace AC.Service.LiCai
{
    public interface IMemberService
    {
        void InitUserMember(int userId);

        void Create(IList<MemberDTO> members);

        int Create(MemberDTO member);

        IList<MemberDTO> GetByUserId(int userId);
    }
}
