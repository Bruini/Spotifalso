using System;
using System.Collections.Generic;

namespace Spotifalso.Aplication.Inputs
{
    public class MusicInput
    {
        public Guid? CoverImageId { get; private set; }
        public string Title { get; private set; }
        public string Lyrics { get; private set; }
        public List<Guid> ArtistsIds { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTime ReleaseDate { get; private set; }
    }
}
