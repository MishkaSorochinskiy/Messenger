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
        }
    }
}
