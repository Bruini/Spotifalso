using Microsoft.EntityFrameworkCore;

namespace Spotifalso.Infrastructure.Data.Config
{
    public class SpotifalsoDBContext : DbContext
    {
        public SpotifalsoDBContext(DbContextOptions<SpotifalsoDBContext> options) : base(options)
        {

        }
       
        //TODO Add DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           //TODO Add Modelbuilder mapping
        }
    }
}
