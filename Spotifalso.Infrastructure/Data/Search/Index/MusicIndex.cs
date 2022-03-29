using Spotifalso.Aplication.ViewModels;
using Spotifalso.Core.Models;
using System;

namespace Spotifalso.Infrastructure.Data.Search.Index
{
    public class MusicIndex
    {
        public Guid Id { get; set; }
        public Guid? CoverImageId { get; set; }
        public string Title { get; set; }
        public string Lyrics { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime ReleaseDate { get; set; }

        public MusicIndex()
        {

        }

        public MusicIndex(Music music)
        {
            Id = music.Id;
            CoverImageId = music.CoverImageId;
            Title = music.Title;    
            Lyrics = music.Lyrics;
            Duration = music.Duration;
            ReleaseDate = music.ReleaseDate;
        }

        public MusicViewModel ConvertToViewModel()
        {
            return new MusicViewModel
            {
                Id = Id,
                CoverImageId = CoverImageId,
                Duration = Duration,
                Lyrics = Lyrics,
                ReleaseDate = ReleaseDate,
                Title = Title,
            };
        }
    }
}
