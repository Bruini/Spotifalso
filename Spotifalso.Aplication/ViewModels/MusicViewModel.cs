using System;
using System.Collections.Generic;

namespace Spotifalso.Aplication.ViewModels
{
    public class MusicViewModel
    {
        public Guid Id { get; set; }
        public Guid? CoverImageId { get; set; }
        public string Title { get; set; }
        public string Lyrics { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<ArtistMusicViewModel> Artists { get; set; }
        public IEnumerable<string> Albuns { get; set; }
    }

    public class ArtistMusicViewModel
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string Name { get; set; }
    }
}
