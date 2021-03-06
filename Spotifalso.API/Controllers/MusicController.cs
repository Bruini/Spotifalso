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
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;

        public MusicController(IMusicService musicService)
        {
            _musicService = musicService;
        }

        [HttpGet]
        public async Task<IEnumerable<MusicViewModel>> Get()
        {
            return await _musicService.GetAllAsync();
        }

        [HttpGet("search")]
        public async Task<IEnumerable<MusicViewModel>> Search([FromQuery] string term)
        {
            return await _musicService.SearchAsync(term);
        }

        [HttpGet("{id}")]
        public async Task<MusicViewModel> Get(Guid id)
        {
            return await _musicService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<MusicViewModel> Post([FromBody] MusicInput musicInput)
        {
            return await _musicService.InsertAsync(musicInput);
        }

        [HttpPut("{id}")]
        public async Task<MusicViewModel> Put(Guid id, [FromBody] MusicInput musicInput)
        {
            return await _musicService.UpdateAsync(id, musicInput);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _musicService.DeleteAsync(id);
            NoContent();
        }
    }
}
