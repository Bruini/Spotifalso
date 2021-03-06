using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Services;
using Spotifalso.Aplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotifalso.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<IEnumerable<ArtistViewModel>> Get()
        {
            return await _artistService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ArtistViewModel> Get(Guid id)
        {
            return await _artistService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ArtistViewModel> Post([FromBody] ArtistInput artistInput)
        {
            return await _artistService.InsertAsync(artistInput);
        }

        [HttpPut("{id}")]
        public async Task<ArtistViewModel> Put(Guid id, [FromBody] ArtistInput artistInput)
        {
            return await _artistService.UpdateAsync(id, artistInput);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _artistService.DeleteAsync(id);
            NoContent();
        }

        [HttpPost("follow/{id}")]
        public async Task<bool> Follow(Guid id, [FromBody] EmailInput emailInput)
        {
            return await _artistService.FollowArtistAsync(id, User, emailInput);
        }
    }
}
