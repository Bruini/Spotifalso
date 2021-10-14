using Spotifalso.Core.Enums;

namespace Spotifalso.Aplication.Inputs
{
    public class UserInput
    {
        public string ProfilePhotoId { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
        public string Nickname { get; set; }
        public string Bio { get; set; }
    }
}
