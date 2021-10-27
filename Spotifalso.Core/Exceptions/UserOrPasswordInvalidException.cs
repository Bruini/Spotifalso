using Spotifalso.Core.Exceptions.Base;

namespace Spotifalso.Core.Exceptions
{
    public class UserOrPasswordInvalidException : NotFoundException
    {
        public UserOrPasswordInvalidException() : base($"User or password is invalid.")
        {
        }
    }
}
