using FluentValidation;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Infrastructure;
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
        private readonly IKeyManagementService _keyManagementService;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserInput> _validator;
        public UserService(IUserRepository userRepository, IKeyManagementService keyManagementService, IValidator<UserInput> validator)
        {
            _keyManagementService = keyManagementService;
            _userRepository = userRepository;
            _validator = validator;
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

            if (user is null)
                throw new UserNotFoundException(id);

            return user;
        }

        public async Task<User> InsertAsync(UserInput userInput)
        {
            //Validate input data
            await _validator.ValidateAndThrowAsync(userInput);

            //TODO validate role

            //Encript user password
            var password = await _keyManagementService.EncriptUserPassword(userInput.Password);

            var user = new User(userInput.ProfilePhotoId, password, userInput.Role, userInput.Nickname, userInput.Bio);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(Guid id, UserInput userInput)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                throw new UserNotFoundException(id);

            //Validate input data
            await _validator.ValidateAndThrowAsync(userInput);

            //TODO validate role

            if (!string.IsNullOrWhiteSpace(userInput.Password))
            {
                var oldPassword = await _keyManagementService.DecriptUserPassword(user.Password);
                if(oldPassword != userInput.Password)
                {
                    var newPassword = await _keyManagementService.EncriptUserPassword(user.Password);
                    user.ChangePassword(newPassword);
                }             
            }

            user.ChangeBio(userInput.Bio);
            user.ChangeNickname(userInput.Nickname);
            user.ChangeProfilePhotoId(userInput.ProfilePhotoId);
          
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return user;
        }
    }
}
