using AutoMapper;
using FluentValidation;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Aplication.Interfaces.Services;
using Spotifalso.Aplication.ViewModels;
using Spotifalso.Core.Enums;
using Spotifalso.Core.Exceptions;
using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Services
{
    public class UserService : IUserService
    {
        private readonly IKeyManagementService _keyManagementService;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserInput> _validator;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IKeyManagementService keyManagementService, IValidator<UserInput> validator, IMapper mapper)
        {
            _keyManagementService = keyManagementService;
            _userRepository = userRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                throw new UserNotFoundException(id);

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        public async Task<UserViewModel> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                throw new UserNotFoundException(id);

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> InsertAsync(UserInput userInput, ClaimsPrincipal userClaims)
        {
            //Validate input data
            await _validator.ValidateAndThrowAsync(userInput);

            var role = ValidateAndComposeRole(userInput, userClaims);

            //Encript user password       
            var password = await _keyManagementService.EncriptUserPassword(userInput.Password);
            var user = new User(userInput.ProfilePhotoId, password, role, userInput.Nickname, userInput.Bio);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserViewModel>(user);
        }

        private Roles ValidateAndComposeRole(UserInput userInput, ClaimsPrincipal userClaims)
        {
            //Validate role exists
            var roleExist = Enum.TryParse(userInput.Role, out Roles role);
            if (!roleExist)
                throw new RoleNotExistsException();

            //Validate role Authorization from token claim
            if (userClaims.Identity.IsAuthenticated == false && role == Roles.Admin)
                throw new RoleForbiddenException();

            if (role == Roles.Admin && !userClaims.IsInRole(Roles.Admin.ToString()))
                throw new RoleForbiddenException();

            return role;
        }

        public async Task<UserViewModel> UpdateAsync(Guid id, UserInput userInput)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                throw new UserNotFoundException(id);

            //Validate input data
            await _validator.ValidateAndThrowAsync(userInput);

            if (!string.IsNullOrWhiteSpace(userInput.Password))
            {
                var oldPassword = await _keyManagementService.DecriptUserPassword(user.Password);
                if (oldPassword != userInput.Password)
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

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
