using Googazon.Library.Messaging;
using Microsoft.Azure.WebJobs;
using NeedFulfillment.Messaging;
using NeedFulfillment.Models;
using System;
using System.Threading.Tasks;

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
