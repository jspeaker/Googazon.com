using GoogazonFunctions.Texts;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;

namespace GoogazonFunctions.Messaging
{
    public class ServiceBusTopic
    {
        private static readonly Dictionary<string, ITopicClient> TopicClients = new Dictionary<string, ITopicClient>();
        private static readonly object LockObject = new object();

        public static ITopicClient Client(string topic)
        {
            // ReSharper disable InconsistentlySynchronizedField
            if (TopicClients.ContainsKey(topic)) return TopicClients[topic];
            // ReSharper restore InconsistentlySynchronizedField

            lock (LockObject)
            {
                if (TopicClients.ContainsKey(topic)) return TopicClients[topic];

                TopicClient topicClient = new TopicClient(Environment.GetEnvironmentVariable(new CustomerServiceRiverConnectionStringKey()), topic);
                TopicClients.Add(topic, topicClient);
                return topicClient;
            }
        }
    }
}