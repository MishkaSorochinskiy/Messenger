using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.ToTable("Conversations");

            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Users)
                .WithOne(u => u.Conversation)
                .HasForeignKey(c => c.ConversationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
