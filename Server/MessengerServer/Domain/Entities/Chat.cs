using System.Collections.Generic;

namespace Domain.Entities
{
    public class  Converation
    {
        public int Id { get; set; }

        public int? LastMessageId { get; set; }
        public Message LastMessage { get; set; }

        public ICollection<Message> Messages { get; private set; }

        public ICollection<User> Users { get; set; }

        public Converation()
        {
            this.Messages = new List<Message>();
        }
    }
}
