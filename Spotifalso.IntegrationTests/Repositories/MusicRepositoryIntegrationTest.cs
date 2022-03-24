using Spotifalso.Core.Models;
using Spotifalso.Infrastructure.Data.Repositories;
using Spotifalso.IntegrationTests.Fixtures;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Spotifalso.IntegrationTests.Repositories
{
    public class MusicRepositoryIntegrationTest : IClassFixture<BaseEfRepoTestFixture>
    {
        private readonly BaseEfRepoTestFixture _fixture;
        public MusicRepositoryIntegrationTest(BaseEfRepoTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Add_Music()
        {
            var musicRepository = new MusicRepository(_fixture.Context);
            var music = new Music(Guid.NewGuid(), "Comfortably Numb", "Letra", new TimeSpan(0, 6, 53), new DateTime(1979, 1, 1));

            var musicFromDB = await musicRepository.AddAsync(music);
            await musicRepository.SaveChangesAsync();

            Assert.NotNull(musicFromDB);
            Assert.NotEqual(Guid.Empty, musicFromDB.Id);
            Assert.Equal(music.Lyrics, musicFromDB.Lyrics);
            Assert.Equal(music.ReleaseDate, musicFromDB.ReleaseDate);
            Assert.Equal(music.Title, musicFromDB.Title);
            Assert.Equal(music.Duration, musicFromDB.Duration);
            Assert.Equal(music.CoverImageId, musicFromDB.CoverImageId);
            Assert.Equal(music.Artists, musicFromDB.Artists);
        }

        [Fact]
        public async Task Should_Get_Music_By_Id()
        {
            var musicRepository = new MusicRepository(_fixture.Context);
            var music = new Music(Guid.NewGuid(), "Comfortably Numb", "Letra", new TimeSpan(0, 6, 53), new DateTime(1979, 1, 1));

            var musicFromDB = await musicRepository.AddAsync(music);
            await musicRepository.SaveChangesAsync();

            var getArtistFromId = await musicRepository.GetByIdAsync(musicFromDB.Id);

            Assert.NotNull(getArtistFromId);
            Assert.Equal(musicFromDB, getArtistFromId);
        }

        [Fact]
        public async Task Should_Delete_Music()
        {
            var musicRepository = new MusicRepository(_fixture.Context);
            var music = new Music(Guid.NewGuid(), "Comfortably Numb", "Letra", new TimeSpan(0, 6, 53), new DateTime(1979, 1, 1));

            var musicFromDB = await musicRepository.AddAsync(music);
            await musicRepository.SaveChangesAsync();

            musicRepository.Delete(musicFromDB);
            await musicRepository.SaveChangesAsync();

            var getArtistFromId = await musicRepository.GetByIdAsync(musicFromDB.Id);

            Assert.Null(getArtistFromId);
        }

        [Fact]
        public async Task Should_Update_Music()
        {
            var musicRepository = new MusicRepository(_fixture.Context);
            var music = new Music(Guid.NewGuid(), "Comfortably Numb", "Letra", new TimeSpan(0, 6, 53), new DateTime(1979, 1, 1));

            var musicFromDB = await musicRepository.AddAsync(music);
            await musicRepository.SaveChangesAsync();

            Assert.Equal(music.Lyrics, musicFromDB.Lyrics);
            Assert.Equal(music.ReleaseDate, musicFromDB.ReleaseDate);
            Assert.Equal(music.Title, musicFromDB.Title);
            Assert.Equal(music.Duration, musicFromDB.Duration);
            Assert.Equal(music.CoverImageId, musicFromDB.CoverImageId);
            Assert.Equal(music.Artists, musicFromDB.Artists);

            musicFromDB.ChangeDuration(new TimeSpan(0, 5, 20));
            musicFromDB.ChangeLyrics("N/A");
            musicFromDB.ChangeReleaseDate(new DateTime(1980, 5, 12));
            musicFromDB.ChangeTitle("New");
            musicFromDB.ChangeCoverImageId(Guid.Parse("2d37a964-b8e3-4572-a41a-8fca23eeccd7"));

            musicFromDB.AddArtist(new Artist("Pink Floyd", "N/A", "Pink Floyd"));
            musicFromDB.AddArtist(new Artist("Pink Floyd Cover", "Cover", "Pink Floyd Cover"));

            musicRepository.Update(musicFromDB);

            var getArtistFromId = await musicRepository.GetByIdAsync(musicFromDB.Id);

            Assert.NotNull(getArtistFromId);

            Assert.Equal("N/A", getArtistFromId.Lyrics);
            Assert.Equal(new DateTime(1980, 5, 12), getArtistFromId.ReleaseDate);
            Assert.Equal("New", getArtistFromId.Title);
            Assert.Equal(new TimeSpan(0, 5, 20), getArtistFromId.Duration);
            Assert.Equal(Guid.Parse("2d37a964-b8e3-4572-a41a-8fca23eeccd7"), getArtistFromId.CoverImageId);
            Assert.Equal(2, getArtistFromId.Artists.Count);
        }

        [Fact]
        public async Task Should_Get_All_Musics()
        {
            var musicRepository = new MusicRepository(_fixture.Context);
            var music1 = new Music(Guid.NewGuid(), "Numb Comfortably", "Letra", new TimeSpan(0, 6, 53), new DateTime(1979, 1, 1));
            var music2 = new  Music(Guid.NewGuid(), "Comfortably Numb", "Letra", new TimeSpan(0, 6, 53), new DateTime(1979, 1, 1));

            await musicRepository.AddAsync(music1);
            await musicRepository.AddAsync(music2);
            await musicRepository.SaveChangesAsync();

            var musics = await musicRepository.GetAllAsync();

            Assert.NotNull(musics);
            Assert.Equal(2, musics.ToList().Count);
        }
    }
}
