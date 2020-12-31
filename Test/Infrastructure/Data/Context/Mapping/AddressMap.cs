using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Data.Context.Mapping
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
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
                   .HasColumnName("LastUpdateUser");

            builder.Property(p => p.Description)
                   .HasColumnType("VARCHAR(200)")
                   .HasColumnName("Description")
                   .IsRequired();

            builder.Property(p => p.Complement)
                   .HasColumnType("VARCHAR(200)")
                   .HasColumnName("Complement");

            builder.Property(p => p.Neighborhood)
                   .HasColumnType("VARCHAR(150)")
                   .HasColumnName("Neighborhood")
                   .IsRequired();

            builder.Property(p => p.Number)
                   .HasColumnType("VARCHAR(50)")
                   .HasColumnName("Number")
                   .IsRequired();

            builder.ToTable("UserSystem");
        }
    }
}
