using Spotifalso.Aplication.Inputs;
using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Services
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetAllAsync();
        Task<Artist> GetByIdAsync(Guid id);
        Task<Artist> InsertAsync(ArtistInput artistInput);
        Task<Artist> UpdateAsync(Guid id, ArtistInput artistInput);
        Task DeleteAsync(Guid id);
        Task<bool> FollowArtistAsync(Guid id, ClaimsPrincipal user, EmailInput emailInput);
    }
}
