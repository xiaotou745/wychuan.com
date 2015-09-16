using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Service.DTO.LiCai
{
    public class MemberDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 消费成员
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否默认成员
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        public static IList<MemberDTO> Default()
        {
            return new List<MemberDTO>
            {
                new MemberDTO {Name = "自己", IsDefault = 1},
                new MemberDTO {Name = "配偶", IsDefault = 0},
                new MemberDTO {Name = "孩子", IsDefault = 0},
                new MemberDTO {Name = "父母", IsDefault = 0},
                new MemberDTO {Name = "家庭公用", IsDefault = 0},
            };
        }
    }
}