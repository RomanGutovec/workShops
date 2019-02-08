using System;
using System.Collections.Generic;
using System.Threading;
using CachingSystem;
using NUnit.Framework;

namespace CachingSystem.Tests
{
    [TestFixture]
    public class CacheTests
    {
        [TestCase("test", "testResult")]
        [TestCase("string", "stringResult")]
        [TestCase("third", null)]
        public void AddTestMethod_AddDifferenStringsByPairs(string key, string value)
        {
            //Arrange
            Cache<string, string> actualCache = new Cache<string, string>(
                new TimeSpan(0, 0, 60),
                50,
                new Dictionary<string, string>()
                {
                    { "0", "0" },
                    { "1", "10" }
                });

            //Act
            actualCache.Add(key, value, new TimeSpan(0, 1, 0));

            //Assert
            Assert.AreEqual(value, actualCache.Get(key));
        }

        [Test]
        public void GetTestMethod_WithTwoStrings_ReturnString_0()
        {
            //Arrange
            Cache<string, string> actualCache = new Cache<string, string>(new TimeSpan(0, 0, 60), 50, new Dictionary<string, string>() { { "0", "0" }, { "1", "10" } });

            //Act & Assert
            Assert.AreEqual("0", actualCache.Get("0"));
        }

        [TestCase("test", 5, ExpectedResult = 5)]
        [TestCase("18", -5, ExpectedResult = -5)]
        [TestCase("97", int.MaxValue, ExpectedResult = int.MaxValue)]

        public int GetTestMethod_StringWithIntCases_ExpectEqual(string key, int value)
        {
            //Arrange
            Cache<string, int> actualCache = new Cache<string, int>(
                new TimeSpan(0, 0, 60),
                50,
                new Dictionary<string, int>()
                {
                    { "1", 18 },
                    { key, value },
                    { "2", 15 }
                });

            //Act & Assert
            return actualCache.Get(key);
        }

        [Test]
        public void GetTestMethod_NullKey_ThrownArgumentNullException()
           => Assert.Throws<ArgumentNullException>(() =>
            {
                new Cache<string, int>(
                    new TimeSpan(0, 0, 60),
                    50,
                    new Dictionary<string, int>()
                    {
                    { "1", 18 },
                    { "2", 15 }
                    }).Get(null);
            });

        [TestCase("test", "resulString")]
        [TestCase("27", "result ")]
        [TestCase("some string", "some result")]
        public void GetTestMethod_StringWithStringButTimeOfLifeIsOver_ExpectsNull(string key, string value)
        {
            //Arrange
            Cache<string, string> actualCache = new Cache<string, string>(
                new TimeSpan(0, 0, 2),
                50,
                new Dictionary<string, string>()
                {
                    { "1", "10" },
                    { key + key, value + value },
                    { "2", "15" },
                    { "testString", string.Empty },
                    { key, value + value },
                    { key + "2", value }
                });

            ///Act
            Thread.Sleep(3000);

            // Assert
            Assert.Throws<ArgumentException>(() => actualCache.Get(key));
        }

        [Test]
        public void AddTestMethod_()
        {
            //Arrange
            Cache<string, string> actualCache = new Cache<string, string>(
                new TimeSpan(0, 0, 60),
                50,
                new Dictionary<string, string>()
                {
                    { "0", "0" },
                    { "1", "10" }
                });

            //Act
            actualCache.Add("2", "20", new TimeSpan(0, 1, 0));

            //Assert
            Assert.AreEqual("20", actualCache.Get("2"));
        }
    }
}
