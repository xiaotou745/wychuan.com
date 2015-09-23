using System;
using System.Collections.Generic;
using AC.Dao.Tools;
using AC.Service.DTO.Tools;
using AC.Service.Tools;
using AC.Util;

namespace AC.Service.Impl.Tools
{
    public class CompanyService : ICompanyService
    {
        private readonly CompanyDao companyDao;

        public CompanyService()
        {
            companyDao = new CompanyDao();
        }

        public int Create(CompanyDTO company)
        {
            AssertUtils.ArgumentNotNull(company, "company");

            return companyDao.Insert(company);
        }

        public IList<CompanyDTO> GetByUserId(int userId)
        {
            AssertUtils.Greater(userId, 0);

            return companyDao.GetByUserId(userId);
        }
    }
}