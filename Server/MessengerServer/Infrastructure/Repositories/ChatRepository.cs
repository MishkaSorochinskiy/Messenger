using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
   public class ChatRepository: Repository<Chat>, IChatRepository
    {
        public ChatRepository(MessengerContext db):base(db)
        {

        }
    }
}
