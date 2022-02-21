using Spotifalso.Core.Models;
using Spotifalso.Infrastructure.Data.Repositories;
using Spotifalso.IntegrationTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Spotifalso.IntegrationTests.Repositories
{
    public class ArtistRepositoryIntegrationTest : IClassFixture<BaseEfRepoTestFixture>
    {
        private readonly BaseEfRepoTestFixture _fixture;
        public ArtistRepositoryIntegrationTest(BaseEfRepoTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Add_Artist()
        {
            var artistRepository = new ArtistRepository(_fixture.Context);
            var artist = new Artist("Pink Floyd", string.Empty, "PinkFloyd");

            var artistFromDB = await artistRepository.AddAsync(artist);
            await artistRepository.SaveChangesAsync();

            Assert.NotNull(artistFromDB);
            Assert.NotEqual(Guid.Empty, artistFromDB.Id);
            Assert.Equal(artist.Name, artistFromDB.Name);
            Assert.Equal(artist.DisplayName, artistFromDB.DisplayName);
            Assert.Equal(artist.Bio, artistFromDB.Bio);
        }

        [Fact]
        public async Task Should_Get_Artist_By_Id()
        {
            var artistRepository = new ArtistRepository(_fixture.Context);
            var artist = new Artist("Pink Floyd", string.Empty, "PinkFloyd");

            var artistFromDB = await artistRepository.AddAsync(artist);
            await artistRepository.SaveChangesAsync();

            var getArtistFromId = await artistRepository.GetByIdAsync(artistFromDB.Id);

            Assert.NotNull(getArtistFromId);
            Assert.Equal(artistFromDB, getArtistFromId);
        }

        [Fact]
        public async Task Should_Delete_Artist()
        {
            var artistRepository = new ArtistRepository(_fixture.Context);
            var artist = new Artist("Pink Floyd", string.Empty, "PinkFloyd");

            var artistFromDB = await artistRepository.AddAsync(artist);
            await artistRepository.SaveChangesAsync();

            artistRepository.Delete(artistFromDB);
            await artistRepository.SaveChangesAsync();

            var getArtistFromId = await artistRepository.GetByIdAsync(artistFromDB.Id);

            Assert.Null(getArtistFromId);
        }

        [Fact]
        public async Task Should_Update_Artist()
        {
            var artistRepository = new ArtistRepository(_fixture.Context);
            var artist = new Artist("Pink Floyd", string.Empty, "PinkFloyd");

            var artistFromDB = await artistRepository.AddAsync(artist);
            await artistRepository.SaveChangesAsync();

            Assert.Equal("Pink Floyd", artistFromDB.DisplayName);
            Assert.Equal("PinkFloyd", artistFromDB.Name);

            artistFromDB.ChangeDisplayName("Iron Maiden");
            artistFromDB.ChangeName("IronMaiden");
            artistRepository.Update(artistFromDB);

            var getArtistFromId = await artistRepository.GetByIdAsync(artistFromDB.Id);

            Assert.NotNull(getArtistFromId);
            Assert.Equal("Iron Maiden", getArtistFromId.DisplayName);
            Assert.Equal("IronMaiden", getArtistFromId.Name);
        }

        [Fact]
        public async Task Should_Get_All_Artists()
        {
            var artistRepository = new ArtistRepository(_fixture.Context);
            var pink = new Artist("Pink Floyd", string.Empty, "PinkFloyd");
            var iron = new Artist("Iron Maiden", string.Empty, "IronMaide");

            await artistRepository.AddAsync(pink);
            await artistRepository.AddAsync(iron);
            await artistRepository.SaveChangesAsync();

            var artists = await artistRepository.GetAllAsync();

            Assert.NotNull(artists);
            Assert.Equal(2, artists.ToList().Count);
        }

        [Fact]
        public async Task Should_Get_By_DisplayName()
        {
            var artistRepository = new ArtistRepository(_fixture.Context);
            var artist = new Artist("Pink Floyd", string.Empty, "PinkFloyd");

            await artistRepository.AddAsync(artist);
            await artistRepository.SaveChangesAsync();

            var artistFromDB = await artistRepository.GetByDisplayName("Pink Floyd");

            Assert.NotNull(artistFromDB);
            Assert.Equal(artist.Name, artistFromDB.Name);
            Assert.Equal(artist.DisplayName, artistFromDB.DisplayName);
            Assert.Equal(artist.Bio, artistFromDB.Bio);
        }

        [Fact]
        public async Task Should_Get_By_Name()
        {
            var artistRepository = new ArtistRepository(_fixture.Context);
            var artist = new Artist("Pink Floyd", string.Empty, "PinkFloyd");

            await artistRepository.AddAsync(artist);
            await artistRepository.SaveChangesAsync();

            var artistFromDB = await artistRepository.GetByName("PinkFloyd");

            Assert.NotNull(artistFromDB);
            Assert.Equal(artist.Name, artistFromDB.Name);
            Assert.Equal(artist.DisplayName, artistFromDB.DisplayName);
            Assert.Equal(artist.Bio, artistFromDB.Bio);
        }

        [Fact]
        public async Task Should_ArtistExist_Returns_True()
        {
            var artistRepository = new ArtistRepository(_fixture.Context);
            var artist = new Artist("Pink Floyd", string.Empty, "PinkFloyd");

            await artistRepository.AddAsync(artist);
            await artistRepository.SaveChangesAsync();

            Assert.True(await artistRepository.ArtistExist("PinkFloyd"));
        }

        [Fact]
        public async Task Should_ArtistExist_Returns_False()
        {
            var ArtistRepository = new ArtistRepository(_fixture.Context);

            Assert.False(await ArtistRepository.ArtistExist("Xpto"));
        }
    }
}
