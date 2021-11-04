using Spotifalso.Core.Exceptions.Base;

namespace Spotifalso.Core.Exceptions
{
    public class RoleForbiddenException : ForbiddenException
    {
        public RoleForbiddenException() : base($"Role not allowed.")
        {
        }
    }
}
