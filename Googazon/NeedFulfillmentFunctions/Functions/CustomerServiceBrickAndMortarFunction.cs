using Googazon.Library.Messaging;
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
            [ServiceBusTrigger("customerservice", "brickandmortar", Connection = "ServiceBusConnectionString")] string message)
        {
            try
            {
                ServiceBusMessage serviceBusMessage = new ServiceBusMessage(message);
                if (serviceBusMessage.IsEnriched()) return;

                await EventHub.Client.SendAsync(serviceBusMessage.EnrichedInstance(new BrickAndMortarState()).AsEventData());
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n\n{e.StackTrace}");
            }
        }
    }
}
