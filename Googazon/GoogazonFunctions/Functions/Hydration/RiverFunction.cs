using GoogazonActivities.Messaging;
using GoogazonActivities.Models;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogazonFunctions.Functions.Hydration
{
    public class RiverFunction
    {
        [FunctionName("RiverFunction")]
        public async Task Run([EventHubTrigger("rapids", Connection = "EventHubConnectionString")] EventData[] events)
        {
            List<Exception> exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    EventMessageBody messageBody = new EventMessageBody(eventData);
                    if (messageBody.IsEventType(EventType.None)) continue;

                    await messageBody.AsServiceBusMessage().SendAsync();
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            if (exceptions.Count > 1) throw new AggregateException(exceptions);

            if (exceptions.Count == 1) throw exceptions.Single();
        }
    }
}
