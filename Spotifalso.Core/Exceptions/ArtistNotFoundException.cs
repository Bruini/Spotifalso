using Spotifalso.Core.Exceptions.Base;
using System;

namespace Spotifalso.Core.Exceptions
{
    public class ArtistNotFoundException : NotFoundException
    {
        public ArtistNotFoundException(Guid artistId) : base($"The artist with the identifier {artistId} was not found.")
        {
        }
        public ArtistNotFoundException() : base($"The artist was not found.")
        {
        }
    }
}
