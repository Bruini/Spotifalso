using Spotifalso.Aplication.Inputs;
using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<User> InsertAsync(UserInput userInput);
        Task<User> UpdateAsync(Guid id, UserInput userInput);
        Task DeleteAsync(Guid id);
    }
}
