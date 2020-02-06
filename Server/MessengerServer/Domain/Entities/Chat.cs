using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Chat
    {
        public int Id { get; set; }

        public int FirstUserId { get; set; }
        public User FirstUser { get; set; }

        public int SecondUserId { get; set; }
        public User SecondUser { get; set; }

        public int? LastMessageId { get; set; }
        public Message LastMessage { get; set; }

        public ICollection<Message> Messages { get; private set; }

        public Chat()
        {
            this.Messages = new List<Message>();
        }

    }
}
