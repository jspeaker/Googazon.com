using Microsoft.Extensions.Caching.Memory;

namespace GoogazonActivities.Caching
{
    public static class InMemoryCache
    {
        private static readonly IMemoryCache MemoryCache = new MemoryCache(new MemoryCacheOptions());

        public static IMemoryCache Instance() => MemoryCache;
    }
}