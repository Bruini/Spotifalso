using Spotifalso.Core.Exceptions.Base;

namespace Spotifalso.Core.Exceptions
{
    public class RoleNotExistsException : InvalidException
    {
        public RoleNotExistsException() : base($"The Role is invalid.")
        {
        }
    }
}
