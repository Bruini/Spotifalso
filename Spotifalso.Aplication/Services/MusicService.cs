using FluentValidation;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Aplication.Interfaces.Services;
using Spotifalso.Core.Exceptions;
using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Services
{
    public class MusicService : IMusicService
    {
        private readonly IMusicRepository _musicRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IValidator<MusicInput> _validator;
        private readonly IMusicSearchService _musicSearchService;
        private readonly IArtistNotificationService _artistNotificationService;

        public MusicService(
            IMusicRepository musicRepository,
            IValidator<MusicInput> validator,
            IMusicSearchService musicSearchService,
            IArtistNotificationService artistNotificationService,
            IArtistRepository artistRepository
            )
        {
            _musicRepository = musicRepository;
            _validator = validator;
            _musicSearchService = musicSearchService;
            _artistNotificationService = artistNotificationService;
            _artistRepository = artistRepository;
        }

        public async Task<Music> InsertAsync(MusicInput musicInput)
        {
            await _validator.ValidateAndThrowAsync(musicInput);

            var music = new Music(musicInput.CoverImageId, musicInput.Title, musicInput.Lyrics, musicInput.Duration, musicInput.ReleaseDate);

            var artists = await _artistRepository.GetAllByIdListAsync(musicInput.ArtistsIds);

            if (artists.Count() != musicInput.ArtistsIds.Count)
                throw new ArtistNotFoundException();

            foreach (var artist in artists)
            {
                music.AddArtist(artist);
            }

            await _musicRepository.AddAsync(music);
            await _musicRepository.SaveChangesAsync();

            await _musicSearchService.Upload(music);
            await NotifyNewMusic(music, artists);

            return music;
        }

        public async Task DeleteAsync(Guid id)
        {
            var music = await _musicRepository.GetByIdAsync(id);
            if (music is null)
                throw new MusicNotFoundException(id);

            _musicRepository.Delete(music);
            await _musicRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Music>> GetAllAsync()
            => await _musicRepository.GetAllAsync();

        public async Task<Music> GetByIdAsync(Guid id)
        {
            var music = await _musicRepository.GetByIdAsync(id);

            if (music is null)
                throw new MusicNotFoundException(id);

            return music;
        }

        public Task<IEnumerable<Music>> SearchAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task<Music> UpdateAsync(Guid id, MusicInput musicInput)
        {
            var music = await _musicRepository.GetByIdAsync(id);
            if (music is null)
                throw new MusicNotFoundException(id);

            await _validator.ValidateAndThrowAsync(musicInput);

            music.ChangeCoverImageId(musicInput.CoverImageId);
            music.ChangeDuration(musicInput.Duration);
            music.ChangeLyrics(musicInput.Lyrics);
            music.ChangeReleaseDate(musicInput.ReleaseDate);
            music.ChangeTitle(musicInput.Title);

            _musicRepository.Update(music);
            await _musicRepository.SaveChangesAsync();

            return music;
        }

        private async Task NotifyNewMusic(Music music, IEnumerable<Artist> artists)
        {
            foreach (var artist in artists)
            {
                await _artistNotificationService.NotifyNews(artist.Id, $"Uma nova música: {music.Title}, acaba de ser lançada pelo {artist.DisplayName}.");
            }
        }
    }
}
