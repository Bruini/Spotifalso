﻿using Microsoft.EntityFrameworkCore;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Core.Models;
using Spotifalso.Infrastructure.Data.Config;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotifalso.Infrastructure.Data.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly SpotifalsoDBContext _context;

        public ArtistRepository(SpotifalsoDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Artist> AddAsync(Artist artist)
        {
            await _context.AddAsync(artist);
            return artist;
        }

        public async Task<bool> ArtistExist(string name)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(a => a.Name == name);
            if (artist is not null)
            {
                if (artist.Name.ToLowerInvariant() == name.ToLowerInvariant())
                    return true;
            }
            return false;
        }

        public void Delete(Artist artist)
        {
            _context.Remove(artist);
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task<Artist> GetByDisplayName(string displayName)
        {
            return await _context.Artists.FirstOrDefaultAsync(u => u.DisplayName == displayName);
        }

        public async Task<Artist> GetByIdAsync(Guid id)
        {
            return await _context.Artists.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Artist> GetByName(string name)
        {
            return await _context.Artists.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Artist Update(Artist artist)
        {
            _context.Update(artist);
            return artist;
        }
    }
}
