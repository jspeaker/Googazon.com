using GoogazonActivities.Caching;
using GoogazonActivities.Configuration;
using GoogazonActivities.Texts;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace GoogazonActivities.Results
{
    public interface IResult
    {
        ExpandoObject Item(string id);
    }

    public class Result : IResult
    {
        private readonly IResultConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public Result() : this(new ResultConfiguration(), InMemoryCache.Instance()) { }

        public Result(IResultConfiguration configuration, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        public ExpandoObject Item(string id)
        {
            dynamic resultItem = _memoryCache.Get<ExpandoObject>(id);
            Task task = new Task(() =>
            {
                while (resultItem is null || !((IDictionary<string, object>) resultItem).ContainsKey(new ResultsFieldName()))
                {
                    Thread.Sleep(_configuration.PollingFrequency());
                    resultItem = _memoryCache.Get<ExpandoObject>(id);
                }
            });

            task.Start();

            if (!task.Wait(_configuration.UniqueResultTimeout())) throw new WebException(new TimeoutMessage());

            InMemoryCache.Instance().Remove(id);
            return resultItem;
        }
    }
}