using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("Photos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Path)
                   .IsRequired();

            builder.Property(p => p.Name)
                  .IsRequired();
        }
    }
}
