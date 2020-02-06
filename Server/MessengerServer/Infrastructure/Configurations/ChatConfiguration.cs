using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chats");

            builder.HasKey(c => c.Id);


             builder.HasOne(c => c.FirstUser)
                .WithMany(u => u.Chats)
                .HasForeignKey(c => c.FirstUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.SecondUser)
                .WithMany()
                .HasForeignKey(c => c.SecondUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
