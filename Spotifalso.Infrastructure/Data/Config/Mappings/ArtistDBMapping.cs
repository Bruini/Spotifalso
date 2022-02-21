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

            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.DisplayName);
            builder.HasIndex(u => u.Name)
                .IsUnique();

            builder.Property(u => u.Id)
                .HasColumnName("ArtistID")
                .IsRequired();

            builder.Property(u => u.DisplayName).HasColumnName("DisplayName")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Bio).HasColumnName("Bio")
                .HasColumnType("varchar")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(u => u.Name).HasColumnName("Name")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
