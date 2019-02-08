using System;
using CachingSystem;
using NUnit.Framework;

namespace CachingSystem.Tests
{
    [TestFixture]
    public class CacheItemTests
    {
        [Test]        
        public void IsExpiredMethod_ExpiredTimeVeryLittle_True()
        {
            CacheItem<string> cacheItem = new CacheItem<string>("value", DateTime.MinValue);

            Assert.IsTrue(cacheItem.IsExpired());
        }

        [Test]
        public void IsExpiredMethod_ExpiredTimeTooBig_False()
        {
            CacheItem<string> cacheItem = new CacheItem<string>("value", DateTime.MaxValue);

            Assert.IsFalse(cacheItem.IsExpired());
        }
    }
}
