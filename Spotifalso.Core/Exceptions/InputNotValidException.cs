using Spotifalso.Core.Exceptions.Base;

namespace Spotifalso.Core.Exceptions
{
    public class InputNotValidException : InvalidException
    {
        public InputNotValidException(string message): base(message)
        {
        }
    }
}
