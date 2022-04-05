using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Services
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistViewModel>> GetAllAsync();
        Task<ArtistViewModel> GetByIdAsync(Guid id);
        Task<ArtistViewModel> InsertAsync(ArtistInput artistInput);
        Task<ArtistViewModel> UpdateAsync(Guid id, ArtistInput artistInput);
        Task DeleteAsync(Guid id);
        Task<bool> FollowArtistAsync(Guid id, ClaimsPrincipal user, EmailInput emailInput);
    }
}
