using Microsoft.Extensions.Caching.Memory;

namespace StudentCoursesSystem.CacheServer
{
    public class CacheService
    {
        private readonly IMemoryCache _cache;
        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public List<T> GetOrSet<T>(string key, Func<List<T>> fetchData, TimeSpan expiration)
        {
            if (!_cache.TryGetValue(key, out List<T> value))
            {
                value = fetchData(); // Fetch data if not cached
                _cache.Set(key, value, expiration);
            }
            return value;
        }

        public T GetOrSet<T>(string key, Func<T> fetchData, TimeSpan expiration)
        {
            if (!_cache.TryGetValue(key, out T value))
            {
                value = fetchData(); // Fetch data if not cached
                _cache.Set(key, value, expiration);
            }
            return value;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
