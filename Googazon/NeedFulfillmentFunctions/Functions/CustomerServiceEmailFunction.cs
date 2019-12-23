using Googazon.Library.Messaging;
using Microsoft.Azure.WebJobs;
using NeedFulfillmentActivities.Messaging;
using System;
using System.Threading.Tasks;
using NeedFulfillmentActivities.Models.Email;

namespace NeedFulfillmentFunctions.Functions
{
    public static class CustomerServiceEmailFunction
    {
        [FunctionName("CustomerServiceEmail")]
        public static async Task Run(
            [ServiceBusTrigger("customerservice", "email", Connection = "ServiceBusConnectionString")] string message)
        {
            try
            {
                ServiceBusMessage serviceBusMessage = new ServiceBusMessage(message);
                if (serviceBusMessage.IsEnriched()) return;

                await EventHub.Client.SendAsync(serviceBusMessage.EnrichedInstance(new EmailState()).AsEventData());
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n\n{e.StackTrace}");
            }
        }
    }
}
