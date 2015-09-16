using System;
using System.Collections.Generic;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
using AC.Service.LiCai;
using AC.Util;

namespace AC.Service.Impl.LiCai
{
    public class MemberService : IMemberService
    {
        private readonly MemberDao memberDao;

        public MemberService()
        {
            memberDao = new MemberDao();
        }

        public void InitUserMember(int userId)
        {
            memberDao.InitUserMember(userId);
        }

        public void Create(IList<MemberDTO> members)
        {
            AssertUtils.ArgumentNotNull(members, "members");
            AssertUtils.Greater(members.Count, 0);

            foreach (var member in members)
            {
                Create(member);
            }
        }

        public int Create(MemberDTO member)
        {
            AssertUtils.ArgumentNotNull(member, "member");

            return memberDao.Insert(member);
        }

        public IList<MemberDTO> GetByUserId(int userId)
        {
            return memberDao.GetByUserId(userId);
        }
    }
}