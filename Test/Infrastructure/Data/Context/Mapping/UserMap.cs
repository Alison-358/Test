using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Data.Context.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.Id)
                   .HasColumnType("INT")
                   .HasColumnName("Id")
                   .IsRequired();

            builder.Property(ur => ur.LastUpdateDate)
                   .IsRequired()
                   .HasColumnType("DATETIME")
                   .HasColumnName("LastUpdateDate");

            builder.Property(ur => ur.LastUpdateUser)
                   .IsRequired()
                   .HasColumnType("VARCHAR(150)")
                   .HasMaxLength(150)
                   .HasColumnName("LastUpdateUser");

            builder.Property(p => p.Name)
                   .HasColumnType("VARCHAR(200)")
                   .HasColumnName("Name")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(p => p.Password)
                   .HasColumnType("VARCHAR(50)")
                   .HasColumnName("Password")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(p => p.Email)
                   .IsUnique();

            builder.Property(p => p.Email)
                   .HasColumnType("VARCHAR(150)")
                   .HasColumnName("Email")
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(p => p.RoleId)
                   .HasColumnType("INT")
                   .HasColumnName("RoleId")
                   .IsRequired();

            builder.ToTable("User");
        }
    }
}
