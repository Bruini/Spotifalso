using Spotifalso.Core.Exceptions.Base;
using System;

namespace Spotifalso.Core.Exceptions
{
    public class MusicNotFoundException : NotFoundException
    {
        public MusicNotFoundException(Guid artistId) : base($"The music with the identifier {artistId} was not found.")
        {
        }
        public MusicNotFoundException() : base($"The music was not found.")
        {
        }
    }
}
