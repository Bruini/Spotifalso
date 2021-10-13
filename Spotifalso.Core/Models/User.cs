using Spotifalso.Core.Enums;
using System;

namespace Spotifalso.Core.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string ProfilePhotoId { get; private set; }
        public string Password { get; private set; }
        public Roles Role { get; private set; }
        public string Nickname { get; private set; }
        public string Bio { get; private set; }

        public User(string profilePhotoId, string password, Roles role, string nickname, string bio)
        {
            Id = Guid.NewGuid();
            ProfilePhotoId = profilePhotoId;
            Password = password;
            Role = role;
            Nickname = nickname;
            Bio = bio;
        }
    }
}
