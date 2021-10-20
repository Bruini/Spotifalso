using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Infrastructure
{
    public interface IKms
    {
        Task<string> EncriptUserPassword(string password);
        Task<string> DecriptUserPassword(string password);
    }
}
