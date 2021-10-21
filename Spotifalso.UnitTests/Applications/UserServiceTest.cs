using AutoMapper;
using Moq;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Aplication.Mapping;
using Spotifalso.Aplication.Services;
using Spotifalso.Aplication.Validators;
using Spotifalso.Aplication.ViewModels;
using Spotifalso.Core.Models;
using System;
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

            _keyManagementServiceMock.Setup(x => x.EncriptUserPassword(It.IsAny<string>())).ReturnsAsync("dGVzdGVhc2Rhc2RzYWRhc2RzYWRhc2Rhc2RzYWRhc3Nzc3Nzc3Nzc3Nzc3Nzc3NzZHNhYWFhYWFhYWFhYWFkc2FhYWFhYWFhYWFhYWE=");

            var userService = new UserService(_userRepositoryMock.Object, _keyManagementServiceMock.Object, _userValidator, _mapper);         
            var user = await userService.InsertAsync(userInput);

            Assert.NotNull(user);
            Assert.IsType<UserViewModel>(user);
            Assert.Equal(userInput.Nickname, user.Nickname);
            Assert.Equal(userInput.Bio, user.Bio);
            Assert.Equal(userInput.ProfilePhotoId, user.ProfilePhotoId);
            Assert.Equal(userInput.Role, user.Role.ToString()) ;
            _keyManagementServiceMock.Verify(x => x.EncriptUserPassword(userInput.Password), Times.Once);
            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
