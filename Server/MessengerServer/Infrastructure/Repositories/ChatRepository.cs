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
   public class ChatRepository: Repository<Conversation>, IChatRepository
    {
        public ChatRepository(MessengerContext db):base(db)
        {
        }

        public async Task<bool> ChatExistAsync(int firstUserId, int secondUserId)
        {
            return (await this.db.Conversations
                .Where(c => (c.FirstUserId == firstUserId && c.SecondUserId == secondUserId)||
                        (c.FirstUserId == secondUserId && c.SecondUserId == firstUserId))
                .CountAsync())==0;
        }

        public async Task<List<Conversation>> GetUserChatsAsync(int userid)
        {
            var res = await this.db.Conversations
                .Where(c => c.SecondUserId == userid || c.FirstUserId == userid)
                .Include(c => c.FirstUser)
                 .ThenInclude(u=>u.Photo)
                .Include(c => c.SecondUser)
                 .ThenInclude(u=>u.Photo)
                .Include(c=>c.LastMessage)
                .OrderByDescending(c=>c.LastMessage.TimeCreated)
                .ToListAsync();

            return res;
                
        }

        public async Task<Conversation> GetChatContentAsync(int id)
        {
           return await this.db.Conversations
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
