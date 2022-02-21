using Spotifalso.Core.Models;
using System;
using Xunit;

namespace Spotifalso.UnitTests.Models
{
    public class ArtistModelTests
    {
        [Fact]
        public void Artist_Should_Create()
        {
            var artist = new Artist("Pink Floyd", string.Empty, "PinkFloyd");

            Assert.NotNull(artist);
            Assert.NotEqual(Guid.Empty, artist.Id);
            Assert.Equal(string.Empty, artist.Bio);
            Assert.Equal("PinkFloyd", artist.Name);
            Assert.Equal("Pink Floyd", artist.DisplayName);
        }

        [Fact]
        public void Artist_Should_Change_DisplayName()
        {
            var artist = new Artist("Pink Floyd", string.Empty, "PinkFloyd");

            artist.ChangeDisplayName("new display name");

            Assert.NotNull(artist);
            Assert.Equal("new display name", artist.DisplayName);
        }

        [Fact]
        public void Artist_Should_Change_ChangeName()
        {
            var artist = new Artist("Pink Floyd", string.Empty, "PinkFloyd");

            artist.ChangeName("new Name");

            Assert.NotNull(artist);
            Assert.Equal("new Name", artist.Name);
        }

        [Fact]
        public void Artist_Should_Change_Bio()
        {
            var artist = new Artist("Pink Floyd", string.Empty, "PinkFloyd");

            artist.ChangeBio("new bio");

            Assert.NotNull(artist);
            Assert.Equal("new bio", artist.Bio);
        }

    }
}
