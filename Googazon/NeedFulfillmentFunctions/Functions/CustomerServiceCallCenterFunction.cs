using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using NeedFulfillmentActivities.Messaging;
using NeedFulfillmentActivities.Models.CallCenter;
using System;
using System.Threading.Tasks;

namespace NeedFulfillmentFunctions.Functions
{
    public static class CustomerServiceCallCenterFunction
    {
        [FunctionName("CustomerServiceCallCenter")]
        public static async Task Run(
            [ServiceBusTrigger("customerservice", "callcenter", Connection = "ServiceBusConnectionString")] string message,
            [EventHub("rapids", Connection = "EventHubConnectionString")] IAsyncCollector<EventData> collector)
        {
            try
            {
                ServiceBusMessage serviceBusMessage = new ServiceBusMessage(message);
                if (serviceBusMessage.IsEnriched()) return;

                await collector.AddAsync(serviceBusMessage.EnrichedInstance(new CallCenterState()).AsEventData());
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n\n{e.StackTrace}");
            }
        }
    }
}
