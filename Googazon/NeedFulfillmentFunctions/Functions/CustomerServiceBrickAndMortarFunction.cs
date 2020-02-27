using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using NeedFulfillmentActivities.Messaging;
using NeedFulfillmentActivities.Models.BrickAndMortar;
using System;
using System.Threading.Tasks;

namespace NeedFulfillmentFunctions.Functions
{
    public static class CustomerServiceBrickAndMortarFunction
    {
        [FunctionName("CustomerServiceBrickAndMortar")]
        public static async Task Run(
            [ServiceBusTrigger("customerservice", "brickandmortar", Connection = "ServiceBusConnectionString")] string message,
            [EventHub("rapids", Connection = "EventHubConnectionString")] IAsyncCollector<EventData> collector)
        {
            try
            {
                ServiceBusMessage serviceBusMessage = new ServiceBusMessage(message);
                if (serviceBusMessage.IsEnriched()) return;

                await collector.AddAsync(serviceBusMessage.EnrichedInstance(new BrickAndMortarState()).AsEventData());
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n\n{e.StackTrace}");
            }
        }
    }
}
