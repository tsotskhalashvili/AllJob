using AllJob.Application.Interfaces.Services.Shared;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace AllJob.API.Services;

public class CacheService(IMemoryCache cache) : ICacheService
{
    private readonly ConcurrentDictionary<string, bool> _keys = new();

    public T? Get<T>(string key)
    {
        cache.TryGetValue(key, out T? value);
        return value;
    }

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        cache.Set(key, value, expiration);
        _keys.TryAdd(key, true);
    }

    public void Remove(string key)
    {
        cache.Remove(key);
        _keys.TryRemove(key, out _);
    }

    public void RemoveByPrefix(string prefix)
    {
        var keys = _keys.Keys
            .Where(k => k.StartsWith(prefix))
            .ToList();

        foreach (var key in keys)
        {
            cache.Remove(key);
            _keys.TryRemove(key, out _);
        }
    }
}