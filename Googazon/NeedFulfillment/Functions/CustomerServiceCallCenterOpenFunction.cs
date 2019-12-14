using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using NeedFulfillment.Models;
using NeedFulfillment.Texts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace NeedFulfillment.Functions
{
    public static class CustomerServiceCallCenterOpenFunction
    {
        [FunctionName("CustomerServiceCallCenterOpen")]
        public static async Task Run([ServiceBusTrigger("callcenteropen", "callcenteropen", Connection = "CustomerServiceRiverConnectionString")]
            string message)
        {
            try
            {
                EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(
                    new EventHubsConnectionStringBuilder(Environment.GetEnvironmentVariable("EventHubConnectionString"))
                    {
                        EntityPath = new RapidsKey(),
                        TransportType = TransportType.AmqpWebSockets
                    }.ToString());

                ServiceBusMessage serviceBusMessage = new ServiceBusMessage(message);

                if (serviceBusMessage.IsEnriched()) return;

                EventData asEventData = serviceBusMessage.EnrichedInstance(new CallCenterState()).AsEventData();
                await eventHubClient.SendAsync(asEventData);

                Console.WriteLine($"C# ServiceBus topic trigger function enriched message: \n\n {message} \n\n as \n\n {serviceBusMessage}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n\n{e.StackTrace}");
            }
        }
    }

    // TODO : write tests around this class
    public class ServiceBusMessage
    {
        private readonly Lazy<dynamic> _lazyMessageObject;

        private dynamic MessageObject => _lazyMessageObject.Value;

        public ServiceBusMessage(string serviceBusMessage) : this(new Lazy<dynamic>(() => JsonConvert.DeserializeObject<ExpandoObject>(serviceBusMessage))) { }

        public ServiceBusMessage(Lazy<dynamic> lazyMessageObject) => _lazyMessageObject = lazyMessageObject;

        public static implicit operator string(ServiceBusMessage instance) => JsonConvert.SerializeObject(instance.MessageObject);

        public bool IsEnriched() => ((IDictionary<string, object>) MessageObject).ContainsKey("Results");

        // TODO: SRP this with strategies - violated guard clause policy
        public ServiceBusMessage EnrichedInstance(dynamic enrichment)
        {
            ServiceBusMessage newMessageObject = new ServiceBusMessage(JsonConvert.SerializeObject(MessageObject));
            if (newMessageObject.IsEnriched())
            {
                ((List<dynamic>) newMessageObject.MessageObject.Results).Add(enrichment);
                return newMessageObject;
            }

            newMessageObject.MessageObject.Results = new List<dynamic> { enrichment };
            return newMessageObject;
        }

        public EventData AsEventData() => new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_lazyMessageObject.Value)));
    }
}
