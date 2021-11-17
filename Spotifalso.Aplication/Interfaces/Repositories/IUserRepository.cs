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
        Task<User> GetByNickName(string nickname);
        User Update(User user);
        void Delete(User user);
        Task<bool> UserExist(string nickname);
        Task SaveChangesAsync();
    }
}
