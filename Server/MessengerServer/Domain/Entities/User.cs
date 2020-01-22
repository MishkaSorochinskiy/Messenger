using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public Photo Photo { get; set; }

        public ICollection<Message> Messages { get; private set; }

        public User()
        {
            Messages = new List<Message>();
        }
    }
}
