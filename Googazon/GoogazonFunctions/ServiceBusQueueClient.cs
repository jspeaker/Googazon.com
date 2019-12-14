using Microsoft.Azure.ServiceBus;
using System;
using Microsoft.Extensions.Caching.Memory;

namespace GoogazonFunctions
{
    public static class ServiceBusQueueClient
    {
        private static readonly IQueueClient QueueClient = new QueueClient(Environment.GetEnvironmentVariable("CustomerServiceRiverConnectionString"), "enrichedmessages");

        public static IQueueClient Instance() => QueueClient;
    }

    public static class InMemoryCache
    {
        private static readonly IMemoryCache MemoryCache = new MemoryCache(new MemoryCacheOptions());

        public static IMemoryCache Instance() => MemoryCache;
    }
}