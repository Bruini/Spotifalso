using Microsoft.EntityFrameworkCore;
using Spotifalso.Core.Models;
using Spotifalso.Infrastructure.Data.Config.Mappings;

namespace Spotifalso.Infrastructure.Data.Config
{
    public class SpotifalsoDBContext : DbContext
    {
        public SpotifalsoDBContext(DbContextOptions<SpotifalsoDBContext> options) : base(options)
        {

        }
       
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Music> Musics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDBMapping());
            modelBuilder.ApplyConfiguration(new ArtistDBMapping());
            modelBuilder.ApplyConfiguration(new MusicDBMapping());
        }
    }
}
