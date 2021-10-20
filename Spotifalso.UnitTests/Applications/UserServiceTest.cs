using Moq;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Aplication.Services;
using Spotifalso.Aplication.Validators;
using Spotifalso.Core.Models;
using System.Threading.Tasks;
using Xunit;

namespace Spotifalso.UnitTests.Applications
{
    public class UserServiceTest
    {
        private readonly Mock<IKeyManagementService> _keyManagementServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserValidator _userValidator;
        public UserServiceTest()
        {
            _keyManagementServiceMock = new Mock<IKeyManagementService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userValidator = new UserValidator();
        }

        [Fact]
        public async Task Should_Insert_User()
        {
            _keyManagementServiceMock.Setup(x => x.EncriptUserPassword(It.IsAny<string>())).ReturnsAsync("dGVzdGVhc2Rhc2RzYWRhc2RzYWRhc2Rhc2RzYWRhc3Nzc3Nzc3Nzc3Nzc3Nzc3NzZHNhYWFhYWFhYWFhYWFkc2FhYWFhYWFhYWFhYWE=");
            var userService = new UserService(_userRepositoryMock.Object, _keyManagementServiceMock.Object, _userValidator);
            var userInput = new UserInput
            {
                Nickname = "Pedro",
                Bio = "Dev",
                Password = "abc001",
                ProfilePhotoId = string.Empty,
                Role = Core.Enums.Roles.Admin
            };

            var user = await userService.InsertAsync(userInput);

            Assert.NotNull(user);
            Assert.Equal(userInput.Nickname, user.Nickname);
            Assert.Equal(userInput.Bio, user.Bio);
            Assert.Equal(userInput.ProfilePhotoId, user.ProfilePhotoId);
            Assert.Equal(userInput.Role, user.Role);
            _keyManagementServiceMock.Verify(x => x.EncriptUserPassword(userInput.Password), Times.Once);
            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
