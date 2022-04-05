using AutoMapper;
using FluentValidation;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Aplication.Interfaces.Services;
using Spotifalso.Aplication.ViewModels;
using Spotifalso.Core.Exceptions;
using Spotifalso.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Spotifalso.Aplication.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IUserService _userService;
        private readonly IValidator<ArtistInput> _validator;
        private readonly IArtistNotificationService _followArtistNotificationService;
        private readonly IMapper _mapper;
        public ArtistService(
            IArtistRepository artistRepository,
            IUserService userService,
            IValidator<ArtistInput> validator,
            IArtistNotificationService followArtistNotificationService,
            IMapper mapper
            )
        {
            _userService = userService;
            _artistRepository = artistRepository;
            _validator = validator;
            _followArtistNotificationService = followArtistNotificationService;
            _mapper = mapper;
        }
        public async Task DeleteAsync(Guid id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist is null)
                throw new ArtistNotFoundException(id);

            _artistRepository.Delete(artist);
            await _artistRepository.SaveChangesAsync();
        }

        public async Task<bool> FollowArtistAsync(Guid id, ClaimsPrincipal claims, EmailInput emailInput)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist is null)
                throw new ArtistNotFoundException(id);

            var userId = Guid.Parse(claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid.ToString()).Value);
            var user = await _userService.GetByIdAsync(userId);
            if (user is null)
                throw new UserNotFoundException(userId);

            return await _followArtistNotificationService.FollowArtist(id, userId, emailInput.Email);
        }

        public async Task<IEnumerable<ArtistViewModel>> GetAllAsync()
            => _mapper.Map<IEnumerable<ArtistViewModel>>(await _artistRepository.GetAllAsync());

        public async Task<ArtistViewModel> GetByIdAsync(Guid id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);

            if (artist is null)
                throw new ArtistNotFoundException(id);

            return _mapper.Map<ArtistViewModel>(artist);
        }

        public async Task<ArtistViewModel> InsertAsync(ArtistInput artistInput)
        {
            await _validator.ValidateAndThrowAsync(artistInput);
            await ValidateArtistExists(artistInput);

            var artist = new Artist(artistInput.DisplayName, artistInput.Bio, artistInput.Name);

            await _artistRepository.AddAsync(artist);
            await _artistRepository.SaveChangesAsync();

            return _mapper.Map<ArtistViewModel>(artist);
        }

        public async Task<ArtistViewModel> UpdateAsync(Guid id, ArtistInput artistInput)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist is null)
                throw new ArtistNotFoundException(id);

            await _validator.ValidateAndThrowAsync(artistInput);

            if (artistInput.Name.ToLowerInvariant() != artist.Name.ToLowerInvariant())
            {
                await ValidateArtistExists(artistInput);
                artist.ChangeName(artistInput.Name);
            }

            artist.ChangeBio(artistInput.Bio);
            artist.ChangeDisplayName(artistInput.DisplayName);

            _artistRepository.Update(artist);
            await _artistRepository.SaveChangesAsync();

            return _mapper.Map<ArtistViewModel>(artist);
        }

        private async Task ValidateArtistExists(ArtistInput artistInput)
        {
            if (await this._artistRepository.ArtistExist(artistInput.Name))
            {
                throw new ArtistAlreadyExistsException();
            }
        }
    }
}
