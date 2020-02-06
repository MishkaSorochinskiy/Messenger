using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{

    public enum Sex
    {
        Male,
        Female
    }
    public class User
    {
        public int Id { get; set; }

        public int Age { get; set; }

        public string NickName { get; set; }

        public string PhoneNumber { get; set; }

        public Sex Sex { get; set; }

        public Photo Photo { get; set; }

        public ICollection<Message> Messages { get; private set; }

        public ICollection<Chat> Chats { get; private set; }

        public ICollection<BlockedUser> BlockedUsers { get; private set; }

        public User()
        {
            Messages = new List<Message>();

            Chats = new List<Chat>();

            BlockedUsers = new List<BlockedUser>();
        }
    }
}
