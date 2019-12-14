using GoogazonFunctions.Models;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogazonFunctions.Functions.Hydration
{
    public class CustomerServiceRiverFunction
    {
        private static ITopicClient _topicClient;

        [FunctionName("CustomerServiceRiverFunction")]
        public async Task Run([EventHubTrigger("rapids", Connection = "EventHubConnectionString")] EventData[] events)
        {
            List<Exception> exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    string messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);

                    CandidateMessage message = JsonConvert.DeserializeObject<CandidateMessage>(messageBody);

                    if (!message.IsEventType(EventType.CustomerService)) continue;

                    Console.WriteLine(messageBody);

                    // TODO: create a collection of TopicClients - 1 for each topic - lazy / on-demand
                    string topic = message.Topic();
                    _topicClient = new TopicClient(Environment.GetEnvironmentVariable("CustomerServiceRiverConnectionString"), topic);

                    Message serviceBusMessage = new Message(Encoding.UTF8.GetBytes(messageBody));
                    await _topicClient.SendAsync(serviceBusMessage);
                    Console.WriteLine($"Wrote {messageBody} to service bus topic '{topic}'.");

                    dynamic messageObject = JsonConvert.DeserializeObject<ExpandoObject>(messageBody);
                    if (!((IDictionary<string, object>) messageObject).ContainsKey("Results")) continue;

                    await ServiceBusQueueClient.Instance().SendAsync(serviceBusMessage);
                    Console.WriteLine($"Wrote {messageBody} to service bus queue 'enrichedmessages'.");
                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
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
