using Spotifalso.Core.Models;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Infrastructure
{
    public interface IMusicSearchService
    {
        Task<bool> Upload(Music music);
    }
}
