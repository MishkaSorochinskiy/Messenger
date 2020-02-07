using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
   public class ChatRepository: Repository<Chat>, IChatRepository
    {
        public ChatRepository(MessengerContext db):base(db)
        {
        }

        public async Task<bool> ChatExist(int firstUserId, int secondUserId)
        {
            return (await this.db.Chats
                .Where(c => c.FirstUserId == firstUserId && c.SecondUserId == secondUserId)
                .CountAsync()) ==0;
        }
    }
}
