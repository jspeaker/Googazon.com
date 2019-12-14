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
        public void Run([ServiceBusTrigger("enrichedmessages", Connection = "CustomerServiceRiverConnectionString")] string serviceBusMessage)
        {
            dynamic messageObject = JsonConvert.DeserializeObject<ExpandoObject>(serviceBusMessage);
            string id = messageObject.id;
            InMemoryCache.Instance().Set(id, (ExpandoObject) messageObject, TimeSpan.FromSeconds(10));

            Console.WriteLine($"{nameof(CustomerServiceResultFunction)} cached message: {serviceBusMessage}");
        }
    }
}
