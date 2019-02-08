using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Unittesting_workshop_examples;

namespace Workshop_unit_tests
{
    [TestFixture]
    public class UnitTest1
    {

        [TestFixture]
        public class TutByFinanceNewsServiceTests
        {
            private Mock<IObjectBroker> _objectBrokerMock;
            private Mock<IRssService> _rssService;


            [SetUp]
            public void SetUp()
            {
                _objectBrokerMock = new Mock<IObjectBroker>();
                _rssService = new Mock<IRssService>();
            }

            [Test]
            public void StoreNews_HostIsNotFinanceTutBy_ExceptionThrow()
            {
                //arange
                var testHost = "https://news.tut.by/rss/sport.rss";

                _rssService.Setup(service => service.GetRss(It.IsAny<string>())).Returns(() => new List<RssModel> {
                new RssModel
                {
                    Link = new Uri("https://finance.tut.by")

                },
                new RssModel
                {
                    Link = new Uri("https://news.tut.by")
                }

            }.AsEnumerable());

                var financeService = new TutByFinanceNewsService(_objectBrokerMock.Object, _rssService.Object);

                //act assert
                Assert.Throws<Exception>(() => financeService.StoreNews(testHost));
            }

            [Test]
            public void StoreNews_ValidUrl_RssServiceCalled()
            {
                //arange
                var financeService = new TutByFinanceNewsService(_objectBrokerMock.Object, _rssService.Object);

                //act 
                financeService.StoreNews("bla");

                //asserr
                _rssService.Verify(service => service.GetRss(It.Is<string>(param => param == "bla")), Times.Once);
            }

            [Test]
            public void StoreNews_ValidUrl_Add()
            {
                //arange
                // var testHost = "https://news.tut.by/rss/sport.rss";

                _rssService.Setup(service => service.GetRss(It.IsAny<string>())).Returns(() => new List<RssModel> {
                new RssModel
                {
                   Link = new Uri("https://finance.tut.by"),
                   Title = "1"
                },
                new RssModel
                {
                    Link = new Uri("https://finance.tut.by"),
                    Title = "2"
                },
                new RssModel
                {
                    Link = new Uri("https://finance.tut.by"),
                    Title = "3"
                }

            }.AsEnumerable());

                var financeService = new TutByFinanceNewsService(_objectBrokerMock.Object, _rssService.Object);

                //act 

                //assert
                _objectBrokerMock.Verify(p => p.Add(It.Is<RssModel>(m => m.Title == "1")), Times.Once);

                _objectBrokerMock.Verify(p => p.Add(It.Is<RssModel>(m => m.Title == "2")), Times.Once);

                _objectBrokerMock.Verify(p => p.Add(It.Is<RssModel>(m => m.Title == "3")), Times.Once);

                // _objectBrokerMock.Verify(p => p.Add(It.IsAny<RssModel>( )), Times.Exactly(3));
            }
        }
    }
}
