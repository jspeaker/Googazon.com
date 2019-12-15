using GoogazonFunctions.Texts;
using Microsoft.Azure.ServiceBus;
using System;

namespace GoogazonFunctions.Messaging
{
    public static class EnrichedMessagesQueue
    {
        private static readonly IQueueClient QueueClient = new QueueClient(Environment.GetEnvironmentVariable(new CustomerServiceRiverConnectionStringKey()), new EnrichedMessagesQueueName());
        public static IQueueClient Client() => QueueClient;
    }
}