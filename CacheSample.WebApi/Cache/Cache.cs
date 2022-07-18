using CacheSample.WebApi.Cache.Providers;

namespace CacheSample.WebApi.Cache
{
    public static class CacheSelector
    {
        public static Redis GetRedis() => new Redis();

        public static Memory GetMemory() => new Memory();
    }
}
