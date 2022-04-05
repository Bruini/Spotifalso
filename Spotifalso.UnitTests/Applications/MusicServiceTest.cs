using AutoMapper;
using Moq;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Aplication.Mapping;
using Spotifalso.Aplication.Services;
using Spotifalso.Aplication.Validators;
using Spotifalso.Core.Exceptions;
using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Spotifalso.UnitTests.Applications
{
    public class MusicServiceTest
    {
        private readonly Mock<IMusicRepository> _musicRepositoryMock;
        private readonly Mock<IArtistRepository> _artistRepositoryMock;
        private readonly Mock<IMusicSearchService> _musicSearchServiceMock;
        private readonly Mock<IArtistNotificationService> _followArtistServiceMock;
        private readonly MusicValidator _musicValidator;
        private readonly IMapper _mapper;
        public MusicServiceTest()
        {
            _musicRepositoryMock = new Mock<IMusicRepository>();
            _artistRepositoryMock = new Mock<IArtistRepository>();
            _musicSearchServiceMock = new Mock<IMusicSearchService>();
            _followArtistServiceMock = new Mock<IArtistNotificationService>();
            _musicValidator = new MusicValidator();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task Should_Get_All_Musics()
        {
            _musicRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(GetFakeMusics());

            var musicService = new MusicService(
                _musicRepositoryMock.Object,
                _musicValidator, 
                _musicSearchServiceMock.Object,
                _followArtistServiceMock.Object,
                _artistRepositoryMock.Object,
                _mapper);

            var musics = await musicService.GetAllAsync();

            Assert.NotEmpty(musics);
            _musicRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Get_Music_By_Id()
        {
            var music = GetFakeMusics().FirstOrDefault();

            _musicRepositoryMock.Setup(x => x.GetByIdAsync(music.Id)).ReturnsAsync(music);

            var musicService = new MusicService(
                _musicRepositoryMock.Object,
                _musicValidator,
                _musicSearchServiceMock.Object,
                _followArtistServiceMock.Object,
                _artistRepositoryMock.Object,
                _mapper);

            var musicFromDB = await musicService.GetByIdAsync(music.Id);

            Assert.NotNull(musicFromDB);
            Assert.Equal(music.Lyrics, musicFromDB.Lyrics);
            Assert.Equal(music.ReleaseDate, musicFromDB.ReleaseDate);
            Assert.Equal(music.Title, musicFromDB.Title);
            Assert.Equal(music.Duration, musicFromDB.Duration);
            Assert.Equal(music.CoverImageId, musicFromDB.CoverImageId);
            _musicRepositoryMock.Verify(x => x.GetByIdAsync(music.Id), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_Music_By_Id()
        {
            var music = GetFakeMusics().FirstOrDefault();

            _musicRepositoryMock.Setup(x => x.GetByIdAsync(music.Id)).ReturnsAsync(music);

            var musicService = new MusicService(
               _musicRepositoryMock.Object,
               _musicValidator,
               _musicSearchServiceMock.Object,
               _followArtistServiceMock.Object,
               _artistRepositoryMock.Object,
               _mapper);

            await musicService.DeleteAsync(music.Id);

            _musicRepositoryMock.Verify(x => x.GetByIdAsync(music.Id), Times.Once);
            _musicRepositoryMock.Verify(x => x.Delete(music), Times.Once);
            _musicRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_Music_By_Id_Expected_MusicNotFoundException()
        {
            var music = GetFakeMusics().FirstOrDefault();

            var musicService = new MusicService(
                _musicRepositoryMock.Object,
                _musicValidator,
                _musicSearchServiceMock.Object,
                _followArtistServiceMock.Object,
                _artistRepositoryMock.Object,
                _mapper);

            var ex = await Assert.ThrowsAsync<MusicNotFoundException>(() => musicService.DeleteAsync(music.Id));

            Assert.Equal($"The music with the identifier {music.Id} was not found.", ex.Message);
            _musicRepositoryMock.Verify(x => x.GetByIdAsync(music.Id), Times.Once);
            _musicRepositoryMock.Verify(x => x.Delete(music), Times.Never);
            _musicRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        private IEnumerable<Music> GetFakeMusics()
        {
            var musics = new List<Music>();

            musics.Add(new Music(Guid.NewGuid(), "Comfortably Numb", "Letra", new TimeSpan(0, 6, 53), new DateTime(1979, 1, 1)));
            musics.Add(new Music(Guid.NewGuid(), "Numb Comfortably", "Letra", new TimeSpan(0, 5, 35), new DateTime(1985, 5, 23)));

            return musics;
        }
    }
}
