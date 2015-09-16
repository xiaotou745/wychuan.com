using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Service.DTO.LiCai;
using AC.Service.Impl.LiCai;
using AC.Service.Impl.User;
using AC.Service.LiCai;
using Common.Logging;
using NUnit.Framework;

namespace AC.Service.Tests.LiCai
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private ICategoryService categoryService;
        private ILog logger;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            categoryService = new CategoryService();
            logger = LogManager.GetCurrentClassLogger();
        }

        [Test]
        public void Init()
        {
            IList<CategoryDTO> lstCategories = CategoryDTO.Default();

            foreach (var category in lstCategories)
            {
                categoryService.Create(category);
            }
        }
    }
}
