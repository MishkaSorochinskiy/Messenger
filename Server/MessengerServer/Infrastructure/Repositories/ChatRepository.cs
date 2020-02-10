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
                .Where(c => (c.FirstUserId == firstUserId && c.SecondUserId == secondUserId)||
                        (c.FirstUserId == secondUserId && c.SecondUserId == firstUserId))
                .CountAsync())==0;
        }

        public async Task<List<Chat>> GetUserChats(int userid)
        {
            var res = await this.db.Chats
                .Where(c => c.SecondUserId == userid || c.FirstUserId == userid)
                .Include(c => c.FirstUser)
                 .ThenInclude(u=>u.Photo)
                .Include(c => c.SecondUser)
                 .ThenInclude(u=>u.Photo)
                .Include(c=>c.LastMessage)
                .ToListAsync();

            return res;
                
        }

        public async Task<Chat> GetChatContent(int id)
        {
           return await this.db.Chats
                 .Where(c => c.Id == id)
                 .Include(c => c.Messages)
                 .Include(c => c.FirstUser)
                     .ThenInclude(u => u.Photo)
                 .Include(c => c.SecondUser)
                     .ThenInclude(u => u.Photo)
                 .FirstOrDefaultAsync();
        }
    }
}
