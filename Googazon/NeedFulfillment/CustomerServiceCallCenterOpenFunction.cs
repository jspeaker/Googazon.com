using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NeedFulfillment
{
    public static class CustomerServiceCallCenterOpenFunction
    {
        [FunctionName("CustomerServiceCallCenterOpen")]
        public static async Task Run([ServiceBusTrigger("callcenteropen", "callcenteropen", Connection = "CustomerServiceRiverConnectionString")]
            string serviceBusMessage)
        {
            try
            {
                EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(
                    new EventHubsConnectionStringBuilder(Environment.GetEnvironmentVariable("EventHubConnectionString"))
                    {
                        EntityPath = "rapids",
                        TransportType = TransportType.AmqpWebSockets
                    }.ToString());

                dynamic messageObject = JsonConvert.DeserializeObject<ExpandoObject>(serviceBusMessage);

                if (((IDictionary<string, object>) messageObject).ContainsKey("Results")) return;

                messageObject.Results = new List<dynamic> { (dynamic) new CallCenterState() };

                string enrichedMessage = JsonConvert.SerializeObject(messageObject);
                await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(enrichedMessage)));

                Console.WriteLine($"C# ServiceBus topic trigger function enriched message: \n\n {serviceBusMessage} \n\n {enrichedMessage}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n\n{e.StackTrace}");
            }
        }

        private class CallCenterState
        {
            public CallCenterState() : this(new CallCenterOpen()) { }

            private CallCenterState(bool open) => _open = open;

            [JsonProperty("source.operation")] private readonly string _sourceOperation = nameof(CustomerServiceCallCenterOpenFunction);

            [JsonProperty("source.assembly")] private readonly string _sourceAssembly = Assembly.GetExecutingAssembly().FullName;

            [JsonProperty("open")] private bool _open;

            [JsonProperty("timestamp")] private readonly DateTime _enrichmentDateTime = DateTime.UtcNow;

            private class CallCenterOpen
            {
                private static bool IsOpen() => DateTime.UtcNow.Hour >= 6 && DateTime.UtcNow.Hour <= 18;

                public static implicit operator bool(CallCenterOpen instance) => IsOpen();
            }
        }
    }
}
