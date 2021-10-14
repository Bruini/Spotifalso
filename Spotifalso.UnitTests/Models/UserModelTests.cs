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
        public void User_Should_Create_With_Excpected_Role(Roles role)
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
        public void User_Should_Change_Nickname()
        {
            var user = new User(string.Empty, "abc001", Roles.Admin, "Admin", "User admin");

            user.ChangeNickname("new nickname");

            Assert.NotNull(user);
            Assert.Equal("new nickname", user.Nickname);
        }

        [Fact]
        public void User_Should_Change_ProfilePhotoId()
        {
            var user = new User(string.Empty, "abc001", Roles.Admin, "Admin", "User admin");

            var profilePhotoId = Guid.NewGuid().ToString();
            user.ChangeProfilePhotoId(profilePhotoId);

            Assert.NotNull(user);
            Assert.Equal(profilePhotoId, user.ProfilePhotoId);
        }

        [Fact]
        public void User_Should_Change_Bio()
        {
            var user = new User(string.Empty, "abc001", Roles.Admin, "Admin", "User admin");

            user.ChangeBio("new bio");

            Assert.NotNull(user);
            Assert.Equal("new bio", user.Bio);
        }

        [Fact]
        public void User_Should_Change_Password()
        {
            var user = new User(string.Empty, "abc001", Roles.Admin, "Admin", "User admin");

            user.ChangePassword("001abc");

            Assert.NotNull(user);
            Assert.Equal("001abc", user.Password);
        }
    }
}
