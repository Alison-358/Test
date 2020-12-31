using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Context.Mapping
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.Id)
                   .HasColumnType("INT")
                   .HasColumnName("Id")
                   .IsRequired();

            builder.HasIndex(p => p.Name)
                   .IsUnique();

            builder.Property(p => p.Name)
                   .HasColumnType("VARCHAR(200)")
                   .HasColumnName("Name")
                   .HasMaxLength(200)
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

            builder.ToTable("Role");
        }
    }
}
