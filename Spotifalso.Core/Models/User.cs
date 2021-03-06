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

        public void ChangeNickname(string nickname)
        {
            if (!string.IsNullOrWhiteSpace(nickname) && nickname != this.Nickname)
                Nickname = nickname;
        }

        public void ChangeProfilePhotoId(string profilePhotoId)
        {
            if (profilePhotoId != this.ProfilePhotoId)
                ProfilePhotoId = profilePhotoId;
        }

        public void ChangeBio(string bio)
        {
            if (bio != this.Bio)
                Bio = bio;
        }

        public void ChangePassword(string password)
        {
            if (!string.IsNullOrWhiteSpace(password) && password != this.Password)
                Password = password;
        }
    }
}
