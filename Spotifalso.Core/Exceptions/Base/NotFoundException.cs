using System;

namespace Spotifalso.Core.Exceptions.Base
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
