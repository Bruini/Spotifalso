using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Spotifalso.UnitTests.Models
{
    public class MusicModelTests
    {
        [Fact]
        public void Music_Should_Create()
        {
            var music = new Music(Guid.NewGuid(), "Comfortably Numb", "Letra", new TimeSpan(0,6,53), new DateTime(1979,1,1));

            Assert.NotNull(music);
            Assert.NotEqual(Guid.Empty, music.Id);
            Assert.Equal("Comfortably Numb", music.Title);
            Assert.Equal("Letra", music.Lyrics);
            Assert.Equal(new TimeSpan(0, 6, 53), music.Duration);
            Assert.Equal(new DateTime(1979, 1, 1), music.ReleaseDate);
        }

        [Fact]
        public void Music_Should_Change_Title()
        {
            var music = new Music(Guid.NewGuid(), "Comfortably Numb", "Letra", new TimeSpan(0, 6, 53), new DateTime(1979, 1, 1));

            music.ChangeTitle("new title");

            Assert.NotNull(music);
            Assert.Equal("new title", music.Title);
        }

        [Fact]
        public void Music_Should_Change_ChangeLyrics()
        {
            var music = new Music(Guid.NewGuid(), "Comfortably Numb", "Letra", new TimeSpan(0, 6, 53), new DateTime(1979, 1, 1));

            music.ChangeLyrics("Nova Letra");

            Assert.NotNull(music);
            Assert.Equal("Nova Letra", music.Lyrics);
        }

        [Fact]
        public void Music_Should_Change_Duration()
        {
            var music = new Music(Guid.NewGuid(), "Comfortably Numb", "Letra", new TimeSpan(0, 6, 53), new DateTime(1979, 1, 1));

            music.ChangeDuration(new TimeSpan(0, 1, 25));

            Assert.NotNull(music);
            Assert.Equal(new TimeSpan(0, 1, 25), music.Duration);
        }

        [Fact]
        public void Music_Should_Change_ReleaseDate()
        {
            var music = new Music(Guid.NewGuid(), "Comfortably Numb", "Letra", new TimeSpan(0, 6, 53), new DateTime(1979, 1, 1));

            music.ChangeReleaseDate(new DateTime(1980, 10, 8));

            Assert.NotNull(music);
            Assert.Equal(new DateTime(1980, 10, 8), music.ReleaseDate);
        }

        [Fact]
        public void Music_Should_Change_CoverImageId()
        {
            var music = new Music(Guid.NewGuid(), "Comfortably Numb", "Letra", new TimeSpan(0, 6, 53), new DateTime(1979, 1, 1));

            music.ChangeCoverImageId(Guid.Parse("01bb0fae-8847-4fd8-b666-912b64151fa9"));

            Assert.NotNull(music);
            Assert.Equal(Guid.Parse("01bb0fae-8847-4fd8-b666-912b64151fa9"), music.CoverImageId);
        }

        private IEnumerable<Artist> FakeArtists()
        {
            var artist = new Artist("Pink Floyd", string.Empty, "PinkFloyd");
            var artists = new List<Artist> { artist };

            return artists;
        }
    }
}
