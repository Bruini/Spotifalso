using System;
using System.Collections;
using System.Collections.Generic;

namespace Spotifalso.Core.Models
{
    public class Artist
    {
        public Guid Id { get; private set; }
        public string DisplayName { get; private set; }
        public string Bio { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<Music> Musics { get; private set; }

        public Artist(string displayName, string bio, string name)
        {
            Id = Guid.NewGuid();
            DisplayName = displayName;
            Bio = bio;
            Name = name;
            Musics = new List<Music>();
        }

        public void ChangeDisplayName(string displayName)
        {
            if (!string.IsNullOrWhiteSpace(displayName) && displayName != this.DisplayName)
                DisplayName = displayName;
        }

        public void ChangeName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name) && name != this.Name)
                Name = name;
        }

        public void ChangeBio(string bio)
        {
            if (bio != this.Bio)
                Bio = bio;
        }
    }
}
