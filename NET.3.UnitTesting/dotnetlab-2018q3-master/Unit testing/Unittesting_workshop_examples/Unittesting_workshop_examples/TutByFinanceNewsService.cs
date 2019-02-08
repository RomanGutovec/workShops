using System;
using System.Collections.Generic;
using System.Linq;

namespace Unittesting_workshop_examples
{

    public class TutByFinanceNewsService
    {
        private const string Host = "finance.tut.by";
        private readonly IObjectBroker inMemoryObjectBroker;
        private readonly IRssService rssService;


        public TutByFinanceNewsService(IObjectBroker inMemoryObjectBroker, IRssService rssService)
        {
            this.inMemoryObjectBroker = inMemoryObjectBroker;
            this.rssService = rssService;
        }

        public IEnumerable<RssModel> StoreNews(string url)
        {
            var modelsToStore = rssService.GetRss(url).ToList();

            // validation
            if (modelsToStore.Any(rssModel => rssModel.Link.Host != Host))
            {
                throw new Exception("Wrong host");
            }

            foreach (var rssModel in modelsToStore)
            {
                inMemoryObjectBroker.Add(rssModel);
            }

            return modelsToStore;
        }
    }

}