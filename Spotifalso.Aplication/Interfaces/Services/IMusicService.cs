using Spotifalso.Aplication.Inputs;
using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Services
{
    public interface IMusicService
    {
        Task<IEnumerable<Music>> GetAllAsync();
        Task<Music> GetByIdAsync(Guid id);
        Task<Music> AddAsync(MusicInput musicInput);
        Task<Music> Update(Guid id, MusicInput musicInput);
        Task Delete(Guid id);
        Task<IEnumerable<Music>> SearchAsync(string searchTerm);
    }
}
