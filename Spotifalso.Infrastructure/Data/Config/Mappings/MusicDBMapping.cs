using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotifalso.Core.Models;

namespace Spotifalso.Infrastructure.Data.Config.Mappings
{
    internal class MusicDBMapping : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> builder)
        {
            builder.ToTable("Musics");

            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Title);

            builder.Property(m => m.Id)
                .HasColumnName("MusicID")
                .IsRequired();

            builder.Property(m => m.CoverImageId)
                .IsRequired(false);

            builder.Property(m => m.Title)
                .HasColumnName("Title")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(m => m.Lyrics)
                .HasColumnName("Lyrics")
                .HasColumnType("text")
                .IsRequired(false);

            builder.Property(m => m.Duration)
                .HasColumnName("Duration")
                .IsRequired(true);

            builder.Property(m => m.ReleaseDate)
                .HasColumnName("ReleaseDate")
                .IsRequired(true);

            builder.HasMany(m => m.Artists)
                .WithMany(m => m.Musics);

            builder.HasMany(m => m.Albums)
                .WithMany(m => m.Songs);
        }
    }
}
