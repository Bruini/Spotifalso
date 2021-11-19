using System;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Services.Caching
{
    public interface IAuthCacheService
    {
        Task SetTokenCacheAsync(Guid userId, string token);
        Task<string> GetTokenCacheAsync(Guid userId);
    }
}
