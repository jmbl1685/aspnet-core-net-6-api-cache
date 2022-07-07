using Newtonsoft.Json;
using System.Runtime.Caching;

namespace CacheSample.WebApi.Cache.Providers
{
    public class Memory : ICacheProvider
    {

        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        private static MemoryCache db = null;


        public Memory()
        {
            if (db is null)
            {
                db = new MemoryCache("cache");
            };
        }

        public async Task Add<T>(string key, T data, int duration = 0)
        {
            try
            {
                string? json = JsonConvert.SerializeObject(data);
                bool check = json is not null && data is not null && key is not null;

                if (check)
                {
                    // cache with expiry
                    if (duration > 0)
                    {
                        var cacheItemPolicy = new CacheItemPolicy
                        {
                            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(duration),

                        };

                        db?.Add(key, json, cacheItemPolicy);
                    }

                    // cache without expiry
                    if (duration <= 0)
                    {
                        var cacheItemPolicy = new CacheItemPolicy();
                        db?.Add(key, json, cacheItemPolicy);
                    }
                }
            }
            catch (ArgumentException err)
            {
                throw new ArgumentException(err?.Message);
            }
        }

        public async Task<T> Get<T>(string key)
        {
            object? data = null;

            try
            {
                var value = db?.Get(key);

                if (value is not null)
                {
                    var jsonParsed = JsonConvert.DeserializeObject(value as string);
                    data = (T)jsonParsed;
                }
            }
            catch (ArgumentException err)
            {
                throw new ArgumentException(err?.Message);
            }

            return (T)data;
        }

    }
}

