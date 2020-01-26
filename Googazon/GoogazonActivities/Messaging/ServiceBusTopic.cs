using GoogazonActivities.Configuration;
using Microsoft.Azure.ServiceBus;
using System.Collections.Generic;

namespace GoogazonActivities.Messaging
{
    public class ServiceBusTopic
    {
        private static readonly Dictionary<string, ITopicClient> TopicClients = new Dictionary<string, ITopicClient>();
        private static readonly object LockObject = new object();

        public static ITopicClient Client(string topic)
        {
            if (TopicClients.ContainsKey(topic)) return TopicClients[topic];

            lock (LockObject)
            {
                if (TopicClients.ContainsKey(topic)) return TopicClients[topic];

                TopicClient topicClient = new TopicClient(new ServiceBusConfiguration().ConnectionString(), topic);
                TopicClients.Add(topic, topicClient);
                return topicClient;
            }
        }
    }
}