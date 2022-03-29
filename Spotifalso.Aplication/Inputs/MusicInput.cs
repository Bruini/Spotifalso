using System;
using System.Collections.Generic;

namespace Spotifalso.Aplication.Inputs
{
    public class MusicInput
    {
        public Guid? CoverImageId { get; set; }
        public string Title { get; set; }
        public string Lyrics { get; set; }
        public List<Guid> ArtistsIds { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
