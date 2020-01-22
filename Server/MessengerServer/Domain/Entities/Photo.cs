using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Photo
    {
        public int Id { get; private set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public byte[] UserPhoto { get; protected set; }
    }
}
