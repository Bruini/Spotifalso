using AutoMapper;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Aplication.Interfaces.Services;
using Spotifalso.Aplication.Interfaces.Services.Caching;
using Spotifalso.Aplication.ViewModels;
using Spotifalso.Core.Exceptions;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IKeyManagementService _keyManagementService;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IAuthCacheService _authCacheService;
        public AuthService(IKeyManagementService keyManagementService, IUserRepository userRepository, ITokenService tokenService, IMapper mapper, IAuthCacheService authCacheService)
        {
            _keyManagementService = keyManagementService;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _authCacheService = authCacheService;
        }

        public async Task<dynamic> Login(LoginInput loginInput)
        {
            var user = await _userRepository.GetByNickName(loginInput.Username);

            if (user is null)
                throw new UserNotFoundException();

            var userPassword = await _keyManagementService.DecriptUserPassword(user.Password);

            if (!string.IsNullOrWhiteSpace(userPassword) && !string.IsNullOrWhiteSpace(loginInput.Password))
            {
                if (loginInput.Password == userPassword)
                {
                    var token = _tokenService.GenerateToken(user);
                    await _authCacheService.SetTokenCacheAsync(user.Id, token);
                    return new
                    {
                        user = _mapper.Map<UserViewModel>(user),
                        token = token
                    };
                }

                throw new UserOrPasswordInvalidException();
            }

            throw new PasswordIsNullException();
        }
    }
}
