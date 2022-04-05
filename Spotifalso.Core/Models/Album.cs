using System;
using System.Collections.Generic;

namespace Spotifalso.Core.Models
{
    public class Album
    {
        public Guid Id { get; private set; }
        public Guid? CoverPhotoId { get; private set; }
        public string Title { get; private set; }
        public Artist Artist { get; private set; }
        public ICollection<Music> Songs { get; private set; }

        public Album(
                Guid? coverPhotoId,
                string title
            )
        {
            Id = Guid.NewGuid();
            CoverPhotoId = coverPhotoId ?? Guid.Empty;
            Title = title;
            Songs = new List<Music>();
        }

        public void ChangeCoverPhotoId(Guid? coverPhotoId)
        {
            if (coverPhotoId != Guid.Empty && coverPhotoId != this.CoverPhotoId)
                CoverPhotoId = coverPhotoId;
        }

        public void ChangeTitle(string title)
        {
            if (!string.IsNullOrWhiteSpace(title) && title != this.Title)
                Title = title;
        }

        public void AddArtist(Artist artist)
        {
            if(artist is not not null)
                Artist = artist;
            else throw new ArgumentNullException("artist", "Artist entity is null");
        }

        public void AddSong(Music music)
        {
            if (music is not null)
                Songs.Add(music);
            else throw new ArgumentNullException("music", "Music entity is null");
        }

        public void RemoveSong(Music music)
        {
            if (music is not null)
                Songs.Remove(music);
            else throw new ArgumentNullException("music", "Music entity is null");
        }
    }
}
