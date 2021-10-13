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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDBMapping());

            #region UserSeed
            modelBuilder.Entity<User>().HasData(new User(string.Empty, "admin", Core.Enums.Roles.Admin, "admin", "Initial Admin user"));
            #endregion
        }
    }
}
