using System;
using System.Collections.Generic;
using GoogazonActivities.Texts.ConfigurationKeys;
using Microsoft.Azure.ServiceBus;

namespace GoogazonActivities.Messaging
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

                TopicClient topicClient = new TopicClient(Environment.GetEnvironmentVariable(new ServiceBusConnectionStringKey()), topic);
                TopicClients.Add(topic, topicClient);
                return topicClient;
            }
        }
    }
}