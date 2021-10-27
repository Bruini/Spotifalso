using Spotifalso.Core.Models;

namespace Spotifalso.Aplication.Interfaces.Infrastructure
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
