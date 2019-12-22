using GoogazonFunctions.Caching;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Dynamic;

namespace GoogazonFunctions.Functions.Results
{
    public class CustomerServiceResultFunction
    {
        [FunctionName("CustomerServiceResultFunction")]
        public void Run([ServiceBusTrigger("result", Connection = "ServiceBusConnectionString")] string serviceBusMessage)
        {
            dynamic messageObject = JsonConvert.DeserializeObject<ExpandoObject>(serviceBusMessage);
            InMemoryCache.Instance().Set((string) messageObject.id, (ExpandoObject) messageObject, TimeSpan.FromSeconds(10));
        }
    }
}
