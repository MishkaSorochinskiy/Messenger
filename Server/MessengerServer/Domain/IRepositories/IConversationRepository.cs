using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IConversationRepository:IRepository<Conversation>
    {
        Task<bool> ChatExistAsync(int firstUserId, int secondUserId);

        Task<List<Conversation>> GetUserChatsAsync(int userid);

        Task<Conversation> GetChatContentAsync(int id);

        Task<Conversation> GetWithConversationAsync(int id);

        Task<List<UserConversation>> GetUsersByChatAsync(int id);
    }
}
