using CacheSample.WebApi.Cache.Providers;

namespace CacheSample.WebApi.Cache
{
    public static class CacheSelector
    {
        public static Redis GetRedis()
        {
            return new Redis();
        }

        public static Memory GetMemory()
        {
            return new Memory();
        }
    }
}
