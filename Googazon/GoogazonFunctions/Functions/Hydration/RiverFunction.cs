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
                    EventMessageBody messageBody = new EventMessageBody(eventData);
                    IEventMessage message = JsonConvert.DeserializeObject<MessageBaseImplementation>(messageBody);
                    if (message.IsEventType(EventType.None)) continue;

                    await new ServiceBusMessage(messageBody, new Topic(messageBody), new Need(messageBody)).SendAsync();
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
