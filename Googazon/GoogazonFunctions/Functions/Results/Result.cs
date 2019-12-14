using GoogazonFunctions.Texts;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace GoogazonFunctions.Functions.Results
{
    public interface IResult
    {
        ExpandoObject Item(string id);
    }

    public class Result : IResult
    {
        public ExpandoObject Item(string id)
        {
            dynamic resultItem = InMemoryCache.Instance().Get<ExpandoObject>(id);
            Task task = new Task(() =>
            {
                while (resultItem is null || !((IDictionary<string, object>) resultItem).ContainsKey(new ResultsKey()))
                {
                    Thread.Sleep(10);
                    resultItem = InMemoryCache.Instance().Get<ExpandoObject>(id);
                }
            });

            task.Start();
            if (!task.Wait(5000)) throw new WebException(new TimeoutMessage());

            InMemoryCache.Instance().Remove(id);
            return resultItem;
        }
    }
}