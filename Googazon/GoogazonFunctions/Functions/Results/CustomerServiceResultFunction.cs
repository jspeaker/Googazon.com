using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using GoogazonFunctions.Caching;

namespace GoogazonFunctions.Functions.Results
{
    public class CustomerServiceResultFunction
    {
        [FunctionName("CustomerServiceResultFunction")]
        public void Run([ServiceBusTrigger("enrichedmessages", Connection = "CustomerServiceRiverConnectionString")] string serviceBusMessage)
        {
            dynamic messageObject = JsonConvert.DeserializeObject<ExpandoObject>(serviceBusMessage);
            InMemoryCache.Instance().Set((string) messageObject.id, (ExpandoObject) messageObject, TimeSpan.FromSeconds(10));
        }
    }
}
