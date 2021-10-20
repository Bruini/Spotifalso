using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Infrastructure
{
    public interface IKeyManagementService
    {
        Task<string> EncriptUserPassword(string password);
        Task<string> DecriptUserPassword(string password);
    }
}
