using Spotifalso.Core.Exceptions.Base;
using System;

namespace Spotifalso.Core.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid userId) : base($"The User with the identifier {userId} was not found.")
        {
        }
        public UserNotFoundException() : base($"The User was not found.")
        {
        }
    }
}
