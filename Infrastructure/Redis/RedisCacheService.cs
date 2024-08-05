using System.Text.Json;
using Application.Interfaces.Cache;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Redis;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    public RedisCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    public async Task<T> GetAsync<T>(string key)
    {
        var cachedValue = await _distributedCache.GetStringAsync(key);
        if (cachedValue == null) return default;

        return JsonSerializer.Deserialize<T>(cachedValue);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        var serializedValue = JsonSerializer.Serialize(value);
        await _distributedCache.SetStringAsync(key, serializedValue, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        });
    }

    public async Task RemoveAsync(string key)
    {
        await _distributedCache.RemoveAsync(key);
    }
}

