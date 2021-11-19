using Microsoft.Extensions.Caching.Distributed;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spotifalso.Infrastructure.Cache
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IDistributedCache _cache;

        public CacheProvider(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetFromCache<T>(string key) where T : class
        {
            var cachedObj = await _cache.GetStringAsync(key);
            return cachedObj == null ? null : JsonSerializer.Deserialize<T>(cachedObj);
        }

        public async Task<string> GetFromCache(string key)
        {
            var cachedString = await _cache.GetStringAsync(key);
            return cachedString == null || string.IsNullOrWhiteSpace(cachedString) ? null : cachedString;
        }

        public async Task SetCache<T>(string key, T value, DistributedCacheEntryOptions options) where T : class
        {
            var cacheObj = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, cacheObj, options);
        }
        public async Task SetCache(string key, string value, DistributedCacheEntryOptions options)
        {
            await _cache.SetStringAsync(key, value, options);
        }

        public async Task ClearCache(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
