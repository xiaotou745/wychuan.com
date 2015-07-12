using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AC.Cache;
using Common.Logging;
using NUnit.Framework;

namespace AC.Tests.Common.Cache
{
    [TestFixture]
    public class DataCacheTests
    {
        private ILog logger = LogManager.GetCurrentClassLogger();

        [Test]
        public void CacheTest()
        {
            DataCache.SetCache("cacheKey", "test");

            object cache = DataCache.GetCache("cacheKey");
            logger.Info(cache);

            DataCache.SetCacheOfAbsolute("key", "test2", DateTime.UtcNow.AddSeconds(5));
            logger.Info(DataCache.GetCache("key"));

            Thread.Sleep(6000);
            logger.Info(DataCache.GetCache("key"));
        }
    }
}