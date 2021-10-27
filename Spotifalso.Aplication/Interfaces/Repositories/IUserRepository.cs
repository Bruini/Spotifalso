using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<User> AddAsync(User user);
        Task<User> GetByNickNameAndPassword(string nickname, string password);
        User Update(User user);
        void Delete(User user);
        Task SaveChangesAsync();
    }
}
