using Spotifalso.Core.Exceptions.Base;

namespace Spotifalso.Core.Exceptions
{
    public class UserAlreadyExistsException : InvalidException
    {
        public UserAlreadyExistsException() : base("User name already exists.")
        {
        }
    }
}
