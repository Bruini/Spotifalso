using Moq;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Aplication.Services;
using Spotifalso.Aplication.Validators;
using Spotifalso.Core.Exceptions;
using Spotifalso.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Spotifalso.UnitTests.Applications
{
    public class ArtistServiceTest
    {
        private readonly Mock<IArtistRepository> _artistRepositoryMock;
        private readonly ArtistValidator _artistValidator;

        public ArtistServiceTest()
        {
            _artistRepositoryMock = new Mock<IArtistRepository>();
            _artistValidator = new ArtistValidator();
        }

        [Fact]
        public async Task Should_Get_All_artists()
        {
            _artistRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(GetFakeArtists());

            var artistService = new ArtistService(_artistRepositoryMock.Object, _artistValidator);

            var artists = await artistService.GetAllAsync();

            Assert.NotEmpty(artists);
            _artistRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Get_artist_By_Id()
        {
            var artist = GetFakeArtists().FirstOrDefault();

            _artistRepositoryMock.Setup(x => x.GetByIdAsync(artist.Id)).ReturnsAsync(artist);

            var artistService = new ArtistService(_artistRepositoryMock.Object, _artistValidator);

            var artistFromDb = await artistService.GetByIdAsync(artist.Id);

            Assert.NotNull(artistFromDb);
            Assert.Equal(artist.Name, artistFromDb.Name);
            Assert.Equal(artist.Bio, artistFromDb.Bio);
            Assert.Equal(artist.DisplayName, artistFromDb.DisplayName);
            _artistRepositoryMock.Verify(x => x.GetByIdAsync(artist.Id), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_artist_By_Id()
        {
            var artist = GetFakeArtists().FirstOrDefault();

            _artistRepositoryMock.Setup(x => x.GetByIdAsync(artist.Id)).ReturnsAsync(artist);

            var artistService = new ArtistService(_artistRepositoryMock.Object, _artistValidator);

            await artistService.DeleteAsync(artist.Id);

            _artistRepositoryMock.Verify(x => x.GetByIdAsync(artist.Id), Times.Once);
            _artistRepositoryMock.Verify(x => x.Delete(artist), Times.Once);
            _artistRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_artist_By_Id_Expected_artistNotFoundException()
        {
            var artist = GetFakeArtists().FirstOrDefault();
            var artistService = new ArtistService(_artistRepositoryMock.Object, _artistValidator);

            var ex = await Assert.ThrowsAsync<ArtistNotFoundException>(() => artistService.DeleteAsync(artist.Id));

            Assert.Equal($"The artist with the identifier {artist.Id} was not found.", ex.Message);
            _artistRepositoryMock.Verify(x => x.GetByIdAsync(artist.Id), Times.Once);
            _artistRepositoryMock.Verify(x => x.Delete(artist), Times.Never);
            _artistRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        private IEnumerable<Artist> GetFakeArtists()
        {
            var artists = new List<Artist>();

            artists.Add(new Artist("Pink Floyd", "Progessive rock band", "Pink Floyd"));
            artists.Add(new Artist("Iron Maiden", "Metal band", "Iron Maiden"));

            return artists;
        }
    }
}
