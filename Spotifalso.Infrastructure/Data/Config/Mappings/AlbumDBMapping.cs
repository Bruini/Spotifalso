using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotifalso.Core.Models;

namespace Spotifalso.Infrastructure.Data.Config.Mappings
{
    internal class AlbumDBMapping : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Albums");

            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Title);

            builder.Property(m => m.Id)
                .HasColumnName("AlbumID")
                .IsRequired();

            builder.Property(m => m.CoverPhotoId)
                .IsRequired(false);

            builder.Property(m => m.Title)
                .HasColumnName("Title")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(m => m.Songs)
             .WithMany(m => m.Albums);

            builder.HasOne(m => m.Artist)
              .WithMany(m => m.Albums);
        }
    }
}
