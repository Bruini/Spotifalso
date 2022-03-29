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
        public ICollection<string> Artists { get; set; }
        public ICollection<string> Albuns { get; set; }
    }
}
