using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    class UserConversationConfiguration : IEntityTypeConfiguration<UserConversation>
    {
        public void Configure(EntityTypeBuilder<UserConversation> builder)
        {
            builder.ToTable("UserConversations");

            builder.HasKey(bu => bu.Id);

            builder.HasOne(uc => uc.Conversation)
                .WithMany(uc => uc.Users)
                .HasForeignKey(uc => uc.ConversationId);

            builder.HasOne(uc => uc.User)
                .WithMany(uc => uc.Conversations)
                .HasForeignKey(uc => uc.UserId);
        }
    }
}
