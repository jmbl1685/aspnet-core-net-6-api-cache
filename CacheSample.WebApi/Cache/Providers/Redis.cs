using Newtonsoft.Json;
using StackExchange.Redis;
using CacheSample.WebApi.Cache;


namespace CacheSample.WebApi.Cache.Providers
{
    public class Redis : ICacheProvider
    {
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        private static ConnectionMultiplexer? redis = null;
        private static IDatabase? db = null;

        public Redis()
        {
            if (redis is null)
            {
                var connectionString = configuration["Redis:ConnectionString"];
                redis = ConnectionMultiplexer.Connect(connectionString);
                db = redis.GetDatabase(9);
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
                        await db?.StringSetAsync(key, json, new TimeSpan(0, duration, 0));
                    }

                    // cache without expiry
                    if (duration <= 0)
                    {
                        await db?.StringSetAsync(key, json);
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
                string value = db?.StringGet(key);

                if (value is not null)
                {
                    var jsonParsed = JsonConvert.DeserializeObject(value);
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
