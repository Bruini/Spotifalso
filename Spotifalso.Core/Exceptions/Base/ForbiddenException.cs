using System;

namespace Spotifalso.Core.Exceptions.Base
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message) { }
    }
}
