using System;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Infrastructure
{
    public interface IFollowArtistNotificationService
    {
        Task<bool> SubscribeArtist(Guid artistId, Guid userId, string emailAddress);
    }
}
