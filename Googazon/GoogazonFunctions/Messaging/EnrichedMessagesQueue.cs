using GoogazonFunctions.Texts;
using Microsoft.Azure.ServiceBus;
using System;

namespace GoogazonFunctions.Messaging
{
    public class EnrichedMessagesQueue
    {
        private static readonly IQueueClient Instance = null;
        private static readonly object LockObject = new object();

        public static IQueueClient Client
        {
            get
            {
                // ReSharper disable once InconsistentlySynchronizedField
                if (Instance != null) return Instance;

                lock (LockObject)
                {
                    if (Instance != null) return Instance;

                    return new QueueClient(Environment.GetEnvironmentVariable(new CustomerServiceRiverConnectionStringKey()), new EnrichedMessagesQueueName());
                }
            }
        }
    }
}