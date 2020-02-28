using System.Collections.Generic;

namespace Domain.Entities
{
    public enum ConversationType
    {
        Chat,
        Group,
        Channel
    }
    public class  Conversation
    {
        public int Id { get; set; }

        public int? LastMessageId { get; set; }
        public Message LastMessage { get; set; }

        public ICollection<Message> Messages { get; private set; }

        public ICollection<UserConversation> Users { get; set; }

        public ConversationType Type { get; set; }
        public ConversationInfo ConversationInfo { get; set; }

        public Conversation()
        {
            this.Messages = new List<Message>();

            this.Users = new List<UserConversation>();
        }
    }
}
