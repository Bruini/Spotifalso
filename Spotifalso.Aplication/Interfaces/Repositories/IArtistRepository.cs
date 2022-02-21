using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Repositories
{
    public interface IArtistRepository
    {
        Task<IEnumerable<Artist>> GetAllAsync();
        Task<Artist> GetByIdAsync(Guid id);
        Task<Artist> AddAsync(Artist artist);
        Task<Artist> GetByName(string name);
        Task<Artist> GetByDisplayName(string displayName);
        Artist Update(Artist artist);
        void Delete(Artist artist);
        Task<bool> ArtistExist(string name);
        Task SaveChangesAsync();
    }
}
