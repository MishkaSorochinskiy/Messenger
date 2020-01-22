using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(MessengerContext db):base(db)
        {

        }
    }
}
