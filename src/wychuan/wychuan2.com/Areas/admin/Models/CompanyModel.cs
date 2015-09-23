using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AC.Service.DTO.Tools;

namespace wychuan2.com.Areas.admin.Models
{
    public class CompanyModel
    {
        /// <summary>
        /// 公司列表
        /// </summary>
        public IList<CompanyDTO> Companys { get; set; }
    }
}