using Microsoft.EntityFrameworkCore;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Core.Models;
using Spotifalso.Infrastructure.Data.Config;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotifalso.Infrastructure.Data.Repositories
{
    public class MusicRepository : IMusicRepository
    {
        private readonly SpotifalsoDBContext _context;
        public MusicRepository(SpotifalsoDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Music> AddAsync(Music music)
        {
            await _context.Musics.AddAsync(music);
            return music;
        }

        public void Delete(Music music)
        {
            _context.Musics.Remove(music);
        }

        public async Task<IEnumerable<Music>> GetAllAsync()
        {
            return await _context.Musics.ToListAsync();
        }

        public async Task<Music> GetByIdAsync(Guid id)
        {
            return await _context.Musics.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Music Update(Music music)
        {
            _context.Musics.Update(music);
            return music;
        }
    }
}
