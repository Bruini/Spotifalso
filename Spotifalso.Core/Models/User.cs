using System;

namespace Spotifalso.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string ProfilePhotoId { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Nickname { get; set; }
        public string Bio { get; set; }
    }
}
