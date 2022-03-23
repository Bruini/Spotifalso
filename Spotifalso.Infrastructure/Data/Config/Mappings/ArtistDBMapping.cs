using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotifalso.Core.Models;

namespace Spotifalso.Infrastructure.Data.Config.Mappings
{
    internal class ArtistDBMapping : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artists");

            builder.HasKey(a => a.Id);
            builder.HasIndex(a => a.DisplayName);
            builder.HasIndex(a => a.Name)
                .IsUnique();

            builder.Property(a => a.Id)
                .HasColumnName("ArtistID")
                .IsRequired();

            builder.Property(a => a.DisplayName).HasColumnName("DisplayName")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Bio).HasColumnName("Bio")
                .HasColumnType("varchar")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(a => a.Name).HasColumnName("Name")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
