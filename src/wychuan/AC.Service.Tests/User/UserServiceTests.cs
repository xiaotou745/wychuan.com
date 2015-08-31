using AC.Service.DTO.User;
using AC.Service.Impl.User;
using AC.Service.User;
using Common.Logging;
using NUnit.Framework;

namespace AC.Service.Tests.User
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService userService;
        private ILog logger;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            userService = new UserService();
            logger = LogManager.GetCurrentClassLogger();
        }

    }
}