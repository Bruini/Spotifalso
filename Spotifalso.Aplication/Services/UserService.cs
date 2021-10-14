using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Aplication.Interfaces.Services;
using Spotifalso.Core.Exceptions;
using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                throw new UserNotFoundException(id);

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if(user is null)
                throw new UserNotFoundException(id);

            return user;
        }

        public async Task<User> InsertAsync(UserInput userInput)
        {
            //TODO validate input

            //TODO validate role

            //TODO encripty password
            var password = userInput.Password;

            var user = new User(userInput.ProfilePhotoId, password, userInput.Role, userInput.Nickname, userInput.Bio);
            user = await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(Guid id, UserInput userInput)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                throw new UserNotFoundException(id);

            //TODO validate input

            //TODO validate role

            //TODO encripty password
            var password = userInput.Password;

            user.ChangeBio(userInput.Bio);
            user.ChangeNickname(userInput.Nickname);
            user.ChangeProfilePhotoId(userInput.ProfilePhotoId);
            user.ChangePassword(password);

            user = _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return user;
        }
    }
}
