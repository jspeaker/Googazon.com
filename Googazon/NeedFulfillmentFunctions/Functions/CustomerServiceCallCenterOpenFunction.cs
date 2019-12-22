using Googazon.Library.Messaging;
using Microsoft.Azure.WebJobs;
using System;
using System.Threading.Tasks;
using NeedFulfillmentActivities.Messaging;
using NeedFulfillmentActivities.Models;

namespace NeedFulfillment.Functions
{
    public static class CustomerServiceCallCenterOpenFunction
    {
        [FunctionName("CustomerServiceCallCenterOpen")]
        public static async Task Run(
            [ServiceBusTrigger("customerservice", "callcenter", Connection = "ServiceBusConnectionString")] string message)
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
}
