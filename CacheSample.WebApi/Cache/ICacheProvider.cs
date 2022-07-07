namespace CacheSample.WebApi.Cache
{
    public interface ICacheProvider
    {
        Task Add<T>(string key, T data, int duration = 0);
        Task<T> Get<T>(string key);
    }
}
