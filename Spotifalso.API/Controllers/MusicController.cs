using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Services;
using Spotifalso.Core.Models;
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
        public async Task<IEnumerable<Music>> Get()
        {
            return await _musicService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Music> Get(Guid id)
        {
            return await _musicService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<Music> Post([FromBody] MusicInput musicInput)
        {
            return await _musicService.InsertAsync(musicInput);
        }

        [HttpPut("{id}")]
        public async Task<Music> Put(Guid id, [FromBody] MusicInput musicInput)
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
