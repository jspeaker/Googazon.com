using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using NeedFulfillment.Models;
using NeedFulfillment.Texts;
using System;
using System.Threading.Tasks;
using NeedFulfillment.Messaging;

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
                ServiceBusMessage serviceBusMessage = new ServiceBusMessage(message);
                if (serviceBusMessage.IsEnriched()) return;

                await EventHub.Client.SendAsync(serviceBusMessage.EnrichedInstance(new CallCenterState()).AsEventData());
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n\n{e.StackTrace}");
            }
        }
    }

    public class EventHub
    {
        private static volatile EventHubClient _instance = null;
        private static readonly object LockObject = new object();

        public static EventHubClient Client
        {
            get
            {
                // ReSharper disable once InconsistentlySynchronizedField
                if (_instance != null) return _instance;

                lock (LockObject)
                {
                    if (_instance != null) return _instance;

                    return EventHubClient.CreateFromConnectionString(
                        new EventHubsConnectionStringBuilder(Environment.GetEnvironmentVariable("EventHubConnectionString"))
                        {
                            EntityPath = new RapidsKey(),
                            TransportType = TransportType.AmqpWebSockets
                        }.ToString());
                }
            }
        }
    }

    // TODO : write tests around this class
}
