using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Service.DTO.Tools
{
    public class CompanyDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户ID，如果为0，则为默认数据
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 内容，简介
        /// </summary>
        public string Introdution { get; set; }

        /// <summary>
        /// 公司创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创始人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}