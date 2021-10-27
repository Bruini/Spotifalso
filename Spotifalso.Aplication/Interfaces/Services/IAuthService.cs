using Spotifalso.Aplication.Inputs;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Services
{
    public interface IAuthService
    {
        Task<dynamic> Login(LoginInput loginInput);
    }
}
