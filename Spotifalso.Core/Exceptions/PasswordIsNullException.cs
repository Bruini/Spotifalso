using Spotifalso.Core.Exceptions.Base;

namespace Spotifalso.Core.Exceptions
{
    public class PasswordIsNullException : InvalidException
    {
        public PasswordIsNullException() : base($"Password is required.")
        {
        }
    }
}
