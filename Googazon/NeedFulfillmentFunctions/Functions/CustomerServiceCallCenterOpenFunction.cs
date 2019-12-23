using Googazon.Library.Messaging;
using Microsoft.Azure.WebJobs;
using NeedFulfillmentActivities.Messaging;
using NeedFulfillmentActivities.Models;
using System;
using System.Threading.Tasks;
using NeedFulfillmentActivities.Models.CallCenter;

namespace NeedFulfillmentFunctions.Functions
{
    public static class CustomerServiceCallCenterFunction
    {
        [FunctionName("CustomerServiceCallCenter")]
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
