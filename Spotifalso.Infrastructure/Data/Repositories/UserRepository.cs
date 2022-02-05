using Microsoft.EntityFrameworkCore;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Core.Models;
using Spotifalso.Infrastructure.Data.Config;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotifalso.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SpotifalsoDBContext _context;

        public UserRepository(SpotifalsoDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByNickName(string nickname)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Nickname == nickname);
        }

        public User Update(User user)
        {
            _context.Update(user);
            return user;
        }

        public async Task<bool> UserExist(string nickname)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Nickname == nickname);
            if (user is not null)
            {
                if (user.Nickname.ToLowerInvariant() == nickname.ToLowerInvariant())
                    return true;
            }
            return false;
              
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
