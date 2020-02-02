using GoogazonActivities.Configuration;
using GoogazonActivities.Texts.ServiceBus;
using Microsoft.Azure.ServiceBus;

// ReSharper disable PossibleMultipleWriteAccessInDoubleCheckLocking
// ReSharper disable ReadAccessInDoubleCheckLocking
namespace GoogazonActivities.Messaging
{
    public static class EnrichedMessagesQueue
    {
        private static IQueueClient Instance;
        private static volatile object LockObject = new object();

        public static IQueueClient Client
        {
            get
            {
                // ReSharper disable once InconsistentlySynchronizedField
                if (Instance != null) return Instance;

                lock (LockObject)
                {
                    if (Instance != null) return Instance;

                    Instance = new QueueClient(new ServiceBusConfiguration().ConnectionString(), new ResultQueueName());
                    return Instance;
                }
            }
        }
    }
}