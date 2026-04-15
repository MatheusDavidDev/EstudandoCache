using Microsoft.Extensions.Caching.Memory;

namespace CacheApi.Services;

public class CacheService(IMemoryCache cache) : ICacheService
{
    private readonly IMemoryCache _cache = cache;

    public object Get(string key)
    {
        if (_cache.TryGetValue(key, out var value))
            return value;

        return null;
    }

    public void Set(string key, object value)
    {
        _cache.Set(key, value, TimeSpan.FromMinutes(5));
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}
