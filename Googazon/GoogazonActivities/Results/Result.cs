using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using GoogazonActivities.Caching;
using GoogazonActivities.Configuration;
using GoogazonActivities.Texts;
using Microsoft.Extensions.Caching.Memory;

namespace GoogazonActivities.Results
{
    public interface IResult
    {
        ExpandoObject Item(string id);
    }

    public class Result : IResult
    {
        private readonly IResultConfiguration _configuration;

        public Result() : this(new ResultConfiguration()) { }

        public Result(IResultConfiguration configuration) => _configuration = configuration;

        public ExpandoObject Item(string id)
        {
            dynamic resultItem = InMemoryCache.Instance().Get<ExpandoObject>(id);
            Task task = new Task(() =>
            {
                while (resultItem is null || !((IDictionary<string, object>) resultItem).ContainsKey(new ResultsFieldName()))
                {
                    // TODO: make this timeout configurable
                    Thread.Sleep(10);
                    resultItem = InMemoryCache.Instance().Get<ExpandoObject>(id);
                }
            });

            task.Start();

            if (!task.Wait(_configuration.UniqueResultTimeout())) throw new WebException(new TimeoutMessage());

            InMemoryCache.Instance().Remove(id);
            return resultItem;
        }
    }
}