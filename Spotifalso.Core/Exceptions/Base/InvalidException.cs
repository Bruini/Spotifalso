using System;

namespace Spotifalso.Core.Exceptions.Base
{
    public class InvalidException : Exception
    {
        public InvalidException(string message) : base(message) { }
    }
}
