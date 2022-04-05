using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.ViewModels;
using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Services
{
    public interface IMusicService
    {
        Task<IEnumerable<MusicViewModel>> GetAllAsync();
        Task<MusicViewModel> GetByIdAsync(Guid id);
        Task<MusicViewModel> InsertAsync(MusicInput musicInput);
        Task<MusicViewModel> UpdateAsync(Guid id, MusicInput musicInput);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<MusicViewModel>> SearchAsync(string searchTerm);
    }
}
