using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotifalso.Core.Enums;
using Spotifalso.Core.Models;
using System;

namespace Spotifalso.Infrastructure.Data.Config.Mappings
{
    internal class UserDBMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Nickname);

            builder.Property(u => u.Id).HasColumnName("UserID")
                .HasColumnType("char")
                .HasMaxLength(36)
                .IsRequired();

            builder.Property(u => u.ProfilePhotoId).HasColumnName("ProfilePhotoId")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(u => u.Nickname).HasColumnName("Nickname")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Password).HasColumnName("Password")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(u => u.Bio).HasColumnName("Bio")
              .HasColumnType("varchar")
              .HasMaxLength(500)
              .IsRequired(false);

            builder.Property(u => u.Role).HasColumnName("Role")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasConversion(
                    r => r.ToString(),
                    r => (Roles)Enum.Parse(typeof(Roles), r))
                .IsRequired();


        }
    }
}
