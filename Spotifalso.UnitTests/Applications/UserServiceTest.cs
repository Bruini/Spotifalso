using AutoMapper;
using Moq;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Aplication.Mapping;
using Spotifalso.Aplication.Services;
using Spotifalso.Aplication.Validators;
using Spotifalso.Aplication.ViewModels;
using Spotifalso.Core.Exceptions;
using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Spotifalso.UnitTests.Applications
{
    public class UserServiceTest
    {
        private readonly Mock<IKeyManagementService> _keyManagementServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IMapper _mapper; 
        private readonly UserValidator _userValidator;
        public UserServiceTest()
        {
            _keyManagementServiceMock = new Mock<IKeyManagementService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userValidator = new UserValidator();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task Should_Insert_User()
        {
            var userInput = new UserInput
            {
                Nickname = "Pedro",
                Bio = "Dev",
                Password = "abc001",
                ProfilePhotoId = string.Empty,
                Role = "Admin"
            };

            var identities = new ClaimsIdentity(new Claim[]
                                   {
                                            new Claim(ClaimTypes.Name, userInput.Nickname),
                                            new Claim(ClaimTypes.Role, userInput.Role.ToString()),
                                   },
                                   "JWT",
                                   ClaimTypes.Name, 
                                   ClaimTypes.Role);

            var claim = new ClaimsPrincipal(identities);

            _keyManagementServiceMock.Setup(x => x.EncriptUserPassword(It.IsAny<string>())).ReturnsAsync("dGVzdGVhc2Rhc2RzYWRhc2RzYWRhc2Rhc2RzYWRhc3Nzc3Nzc3Nzc3Nzc3Nzc3NzZHNhYWFhYWFhYWFhYWFkc2FhYWFhYWFhYWFhYWE=");

            var userService = new UserService(_userRepositoryMock.Object, _keyManagementServiceMock.Object, _userValidator, _mapper);         
            var user = await userService.InsertAsync(userInput, claim);

            Assert.NotNull(user);
            Assert.IsType<UserViewModel>(user);
            Assert.Equal(userInput.Nickname, user.Nickname);
            Assert.Equal(userInput.Bio, user.Bio);
            Assert.Equal(userInput.ProfilePhotoId, user.ProfilePhotoId);
            Assert.Equal(userInput.Role, user.Role);
            _keyManagementServiceMock.Verify(x => x.EncriptUserPassword(userInput.Password), Times.Once);
            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Update_User()
        {
            var userId = Guid.NewGuid();
            var userInput = new UserInput
            {
                Nickname = "Pedro",
                Bio = "Dev",
                Password = "abc001",
                ProfilePhotoId = string.Empty,
                Role = "Admin"
            };

            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(GetFakeUsers().FirstOrDefault());

            _keyManagementServiceMock.Setup(x => x.DecriptUserPassword(It.IsAny<string>())).ReturnsAsync("xpto99");
            _keyManagementServiceMock.Setup(x => x.EncriptUserPassword(It.IsAny<string>())).ReturnsAsync("dGVzdGVhc2Rhc2RzYWRhc2RzYWRhc2Rhc2RzYWRhc3Nzc3Nzc3Nzc3Nzc3Nzc3NzZHNhYWFhYWFhYWFhYWFkc2FhYWFhYWFhYWFhYWE=");

            var userService = new UserService(_userRepositoryMock.Object, _keyManagementServiceMock.Object, _userValidator, _mapper);
            var user = await userService.UpdateAsync(userId, userInput);

            Assert.NotNull(user);
            Assert.IsType<UserViewModel>(user);
            Assert.Equal(userInput.Nickname, user.Nickname);
            Assert.Equal(userInput.Bio, user.Bio);
            Assert.Equal(userInput.ProfilePhotoId, user.ProfilePhotoId);
            Assert.Equal(userInput.Role, user.Role);
            _keyManagementServiceMock.Verify(x => x.DecriptUserPassword(It.IsAny<string>()), Times.Once);
            _keyManagementServiceMock.Verify(x => x.EncriptUserPassword(It.IsAny<string>()), Times.Once);
            _userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Get_All_Users()
        {
            _userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(GetFakeUsers());

            var userService = new UserService(_userRepositoryMock.Object, _keyManagementServiceMock.Object, _userValidator, _mapper);

            var users = await userService.GetAllAsync();

            Assert.NotEmpty(users);
            _userRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Get_User_By_Id()
        {
            var user = GetFakeUsers().FirstOrDefault();

            _userRepositoryMock.Setup(x => x.GetByIdAsync(user.Id)).ReturnsAsync(user);

            var userService = new UserService(_userRepositoryMock.Object, _keyManagementServiceMock.Object, _userValidator, _mapper);

            var userVm = await userService.GetByIdAsync(user.Id);

            Assert.NotNull(userVm);
            Assert.Equal(user.Nickname, userVm.Nickname);
            Assert.Equal(user.Bio, userVm.Bio);
            Assert.Equal(user.ProfilePhotoId, userVm.ProfilePhotoId);
            Assert.Equal(user.Role.ToString(), userVm.Role);
            _userRepositoryMock.Verify(x => x.GetByIdAsync(user.Id), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_User_By_Id()
        {
            var user = GetFakeUsers().FirstOrDefault();

            _userRepositoryMock.Setup(x => x.GetByIdAsync(user.Id)).ReturnsAsync(user);

            var userService = new UserService(_userRepositoryMock.Object, _keyManagementServiceMock.Object, _userValidator, _mapper);

            await userService.DeleteAsync(user.Id);

            _userRepositoryMock.Verify(x => x.GetByIdAsync(user.Id), Times.Once);
            _userRepositoryMock.Verify(x => x.Delete(user), Times.Once);
            _userRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_User_By_Id_Expected_UserNotFoundException()
        {
            var user = GetFakeUsers().FirstOrDefault();
            var userService = new UserService(_userRepositoryMock.Object, _keyManagementServiceMock.Object, _userValidator, _mapper);

            var ex = await Assert.ThrowsAsync<UserNotFoundException>(() => userService.DeleteAsync(user.Id));

            Assert.Equal($"The User with the identifier {user.Id} was not found.", ex.Message);
            _userRepositoryMock.Verify(x => x.GetByIdAsync(user.Id), Times.Once);
            _userRepositoryMock.Verify(x => x.Delete(user), Times.Never);
            _userRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        private IEnumerable<User> GetFakeUsers()
        {
            var users = new List<User>();

            users.Add(new User(string.Empty, "abc", Core.Enums.Roles.Admin, "test_Admin", string.Empty));
            users.Add(new User(string.Empty, "def", Core.Enums.Roles.Subscriber, "test_Subscriber", string.Empty));

            return users;
        }
    }
}
