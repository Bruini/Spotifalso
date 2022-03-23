using System;
using System.Collections.Generic;

namespace Spotifalso.Core.Models
{
    public class Music
    {
        public Guid Id { get; private set; }
        public Guid? CoverImageId { get; private set; }
        public string Title { get; private set; }
        public string Lyrics { get; private set; }
        public List<Artist> Artists { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTime ReleaseDate { get; private set; }

        public Music(
            Guid? coverImageId,
            string title,
            string lyrics,
            TimeSpan duration,
            DateTime releaseDate)
        {
            Id = Guid.NewGuid();
            CoverImageId = coverImageId ?? Guid.Empty;
            Title = title;
            Lyrics = lyrics;
            Artists = new List<Artist>();
            Duration = duration;
            ReleaseDate = releaseDate;
        }

        public void ChangeCoverImageId(Guid coverImageId)
        {
            if (coverImageId != Guid.Empty && coverImageId != this.CoverImageId)
                CoverImageId = coverImageId;
        }

        public void ChangeTitle(string title)
        {
            if (!string.IsNullOrWhiteSpace(title) && title != this.Title)
                Title = title;
        }

        public void ChangeLyrics(string lyrics)
        {
            if (!string.IsNullOrWhiteSpace(lyrics) && lyrics != this.Lyrics)
                Lyrics = lyrics;
        }

        public void ChangeDuration(TimeSpan duration)
        {
            if (duration.TotalSeconds > 0 && duration != this.Duration)
                Duration = duration;
        }

        public void ChangeReleaseDate(DateTime releaseDate)
        {
            if (releaseDate > DateTime.MinValue && releaseDate != this.ReleaseDate)
                ReleaseDate = releaseDate;
        }

        public void AddArtist(Artist artist)
        {
            if (artist is not null)
                Artists.Add(artist);
            else throw new ArgumentNullException("artist", "Artist entity is null");
        }

        public void RemoveArtist(Artist artist)
        {
            if (artist is not null)
                Artists.Remove(artist);
            else throw new ArgumentNullException("artist", "Artist entity is null");
        }
    }
}
