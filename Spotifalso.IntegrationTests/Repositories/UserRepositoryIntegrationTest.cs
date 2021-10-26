using Spotifalso.Core.Models;
using Spotifalso.Infrastructure.Data.Repositories;
using Spotifalso.IntegrationTests.Fixtures;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Spotifalso.IntegrationTests.Repositories
{
    public class UserRepositoryIntegrationTest : IClassFixture<BaseEfRepoTestFixture>
    {
        private readonly BaseEfRepoTestFixture _fixture;
        public UserRepositoryIntegrationTest(BaseEfRepoTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Add_User()
        {
            var userRepository = new UserRepository(_fixture.Context);
            var user = new User(string.Empty, "abc001", Core.Enums.Roles.Admin, "Admin", "Admin bio");

            var userFromDB = await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            Assert.NotNull(userFromDB);
            Assert.NotEqual(Guid.Empty, userFromDB.Id);
            Assert.Equal(user.ProfilePhotoId, userFromDB.ProfilePhotoId);
            Assert.Equal(user.Password, userFromDB.Password);
            Assert.Equal(user.Role, userFromDB.Role);
            Assert.Equal(user.Nickname, userFromDB.Nickname);
            Assert.Equal(user.Bio, userFromDB.Bio);
        }

        [Fact]
        public async Task Should_Get_User_By_Id()
        {
            var userRepository = new UserRepository(_fixture.Context);
            var user = new User(string.Empty, "abc001", Core.Enums.Roles.Admin, "Admin", "Admin bio");

            var userFromDB = await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            var getUserFromId = await userRepository.GetByIdAsync(userFromDB.Id);

            Assert.NotNull(getUserFromId);
            Assert.Equal(userFromDB, getUserFromId);
        }

        [Fact]
        public async Task Should_Delete_User()
        {
            var userRepository = new UserRepository(_fixture.Context);
            var user = new User(string.Empty, "abc001", Core.Enums.Roles.Admin, "Admin", "Admin bio");

            var userFromDB = await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            userRepository.Delete(userFromDB);
            await userRepository.SaveChangesAsync();

            var getUserFromId = await userRepository.GetByIdAsync(userFromDB.Id);

            Assert.Null(getUserFromId);
        }

        [Fact]
        public async Task Should_Update_User()
        {
            var userRepository = new UserRepository(_fixture.Context);
            var user = new User(string.Empty, "abc001", Core.Enums.Roles.Admin, "Admin", "Admin bio");

            var userFromDB = await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            Assert.Equal("Admin", userFromDB.Nickname);

            userFromDB.ChangeNickname("changed");
            userRepository.Update(userFromDB);

            var getUserFromId = await userRepository.GetByIdAsync(userFromDB.Id);

            Assert.NotNull(getUserFromId);
            Assert.Equal("changed", getUserFromId.Nickname);
        }

        [Fact]
        public async Task Should_Get_All_Users()
        {
            var userRepository = new UserRepository(_fixture.Context);
            var admin = new User(string.Empty, "abc001", Core.Enums.Roles.Admin, "Admin", "Admin bio");
            var subscriber = new User(string.Empty, "001abc", Core.Enums.Roles.Subscriber, "Subscriber", "Subscriber bio");

            await userRepository.AddAsync(admin);
            await userRepository.AddAsync(subscriber);
            await userRepository.SaveChangesAsync();

            var users = await userRepository.GetAllAsync();

            Assert.NotNull(users);
            Assert.Equal(2, users.ToList().Count);
        }


    }
}

