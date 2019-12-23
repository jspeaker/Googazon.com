using GoogazonActivities.Messaging;
using GoogazonActivities.Models;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogazonFunctions.Functions.Hydration
{
    public class RiverFunction
    {
        [FunctionName("RiverFunction")]
        public async Task Run(
            [EventHubTrigger("rapids", Connection = "EventHubConnectionString")] EventData[] events)
        {
            List<Exception> exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    EventMessageBody eventMessageBody = new EventMessageBody(eventData);
                    IEventMessage message = JsonConvert.DeserializeObject<MessageBaseImplementation>(eventMessageBody);
                    if (message.IsEventType(EventType.None)) continue;

                    await new ServiceBusMessage(eventMessageBody, message.Topic(), message.Need()).SendAsync();
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }
    }
}
