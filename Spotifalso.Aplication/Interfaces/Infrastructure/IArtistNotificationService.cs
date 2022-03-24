using System;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Infrastructure
{
    public interface IArtistNotificationService
    {
        Task<bool> FollowArtist(Guid artistId, Guid userId, string emailAddress);
        Task NotifyNews(Guid artistId, string message);
    }
}
