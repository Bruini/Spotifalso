using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Infrastructure
{
    public interface ICacheProvider
    {
        Task<T> GetFromCache<T>(string key) where T : class;
        Task<string> GetFromCache(string key);
        Task SetCache<T>(string key, T value, DistributedCacheEntryOptions options) where T : class;
        Task SetCache(string key, string value, DistributedCacheEntryOptions options);
        Task ClearCache(string key);
    }
}
