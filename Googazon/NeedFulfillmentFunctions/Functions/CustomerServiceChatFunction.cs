using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using NeedFulfillmentActivities.Messaging;
using NeedFulfillmentActivities.Models.Chat;
using System;
using System.Threading.Tasks;

namespace NeedFulfillmentFunctions.Functions
{
    public static class CustomerServiceChatFunction
    {
        [FunctionName("CustomerServiceChat")]
        public static async Task Run(
            [ServiceBusTrigger("customerservice", "chat", Connection = "ServiceBusConnectionString")] string message,
            [EventHub("rapids", Connection = "EventHubConnectionString")] IAsyncCollector<EventData> collector)
        {
            try
            {
                ServiceBusMessage serviceBusMessage = new ServiceBusMessage(message);
                if (serviceBusMessage.IsEnriched()) return;

                await collector.AddAsync(serviceBusMessage.EnrichedInstance(new ChatState()).AsEventData());
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n\n{e.StackTrace}");
            }
        }
    }
}
