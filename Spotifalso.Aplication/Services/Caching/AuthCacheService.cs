using Microsoft.Extensions.Caching.Distributed;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Aplication.Interfaces.Services.Caching;
using System;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Services.Caching
{
    public class AuthCacheService : IAuthCacheService
    {
        private const int TTL_Minutes = 60;
        private const string AliasKey = "UserToken";
        private readonly ICacheProvider _cacheProvider;

        public AuthCacheService(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public async Task SetTokenCacheAsync(Guid userId, string token)
        {
            var cacheEntryOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(TTL_Minutes));

            var tokenFromCache = await GetTokenCacheAsync(userId);

            if(tokenFromCache is not null && !string.IsNullOrWhiteSpace(tokenFromCache))
            {
                await _cacheProvider.ClearCache(ComposeKey(userId));
            }

            await _cacheProvider.SetCache(ComposeKey(userId), token, cacheEntryOptions);
        }

        public async Task<string> GetTokenCacheAsync(Guid userId)
        {
            return await _cacheProvider.GetFromCache(ComposeKey(userId));
        }

        private string ComposeKey(Guid userId) => $"{AliasKey}-{userId}";
    }
}
