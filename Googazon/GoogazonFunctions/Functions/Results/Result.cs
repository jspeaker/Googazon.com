using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogazonFunctions.Functions.Results
{
    public interface IResult
    {
        EventData Item(string id);
    }

    public class Result : IResult
    {
        public EventData Item(string id)
        {
            MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
            EventData resultItem = memoryCache.Get<EventData>(id);
            Task task = new Task(() =>
            {
                while (resultItem?.Body.Array == null || resultItem.Body.Array.Length == 0)
                {
                    Thread.Sleep(10);
                    resultItem = memoryCache.Get<EventData>(id);
                }
            });

            task.Start();

            if (!task.Wait(5000)) return new EventData(Encoding.UTF8.GetBytes("{'status':'error','reason':'timeout','description':'Need fulfillment did not occur with 5000ms.'}"));

            memoryCache.Remove(id);
            return resultItem;
        }
    }
}