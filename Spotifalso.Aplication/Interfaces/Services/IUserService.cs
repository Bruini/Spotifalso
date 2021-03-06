using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Spotifalso.Aplication.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserViewModel> GetByIdAsync(Guid id);
        Task<UserViewModel> InsertAsync(UserInput userInput, ClaimsPrincipal UserClaims);
        Task<UserViewModel> UpdateAsync(Guid id, UserInput userInput);
        Task DeleteAsync(Guid id);
    }
}
