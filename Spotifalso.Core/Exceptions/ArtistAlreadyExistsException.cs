using Spotifalso.Core.Exceptions.Base;

namespace Spotifalso.Core.Exceptions
{
    public class ArtistAlreadyExistsException : InvalidException
    {
        public ArtistAlreadyExistsException() : base("Artist name already exists.")
        {
        }
    }
}
