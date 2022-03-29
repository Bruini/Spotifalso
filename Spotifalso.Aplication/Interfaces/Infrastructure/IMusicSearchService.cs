using Spotifalso.Aplication.ViewModels;
using Spotifalso.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Infrastructure
{
    public interface IMusicSearchService
    {
        Task<bool> IndexAsync(Music music);
        Task<IEnumerable<MusicViewModel>> SearchInAllFields(string term);
    }
}
