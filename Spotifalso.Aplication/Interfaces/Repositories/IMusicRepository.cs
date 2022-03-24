using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Repositories
{
    public interface IMusicRepository
    {
        Task<IEnumerable<Music>> GetAllAsync();
        Task<Music> GetByIdAsync(Guid id);
        Task<Music> AddAsync(Music music);
        Music Update(Music music);
        void Delete(Music music);
        Task SaveChangesAsync();
    }
}
