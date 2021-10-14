using Spotifalso.Core.Enums;
using Spotifalso.Core.Models;
using System;
using Xunit;

namespace Spotifalso.UnitTests.Models
{

    public class UserModelTests
    {

        [Theory]
        [InlineData(Roles.Admin)]
        [InlineData(Roles.Subscriber)]
        public void User_Constructor_Should_Create_With_Excpected_Role(Roles role)
        {
            var user = new User(string.Empty, "abc001", role, "Admin", "User admin");

            Assert.NotNull(user);
            Assert.NotEqual(Guid.Empty, user.Id);
            Assert.Equal(string.Empty, user.ProfilePhotoId);
            Assert.Equal("abc001", user.Password);
            Assert.Equal(role, user.Role);
            Assert.Equal("Admin", user.Nickname);
            Assert.Equal("User admin", user.Bio);
        }

        [Fact]
        public void User_With_Empty_Nickname_Should_Throws_Exception()
        {
            var ex = Assert.Throws<Exception>(() => new User(string.Empty, "abc001", Roles.Admin, string.Empty, "User admin"));
            Assert.Equal("Nickname is required", ex.Message);
        }

        [Fact]
        public void User_With_Nickname_More_Than_100_Characters_Should_Throws_Exception()
        {
            string nickname = string.Empty;
            for (int i = 0; i < 101; i++)
            {
                nickname += "a";
            }

            var ex = Assert.Throws<Exception>(() => new User(string.Empty, "abc001", Roles.Admin, nickname, "User admin"));
            Assert.Equal("The maximum length of nickname is 100 characters", ex.Message);
        }
    }
}
