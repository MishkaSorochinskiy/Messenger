using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IChatRepository:IRepository<Chat>
    {
        Task<bool> ChatExistAsync(int firstUserId, int secondUserId);

        Task<List<Chat>> GetUserChatsAsync(int userid);

        Task<Chat> GetChatContentAsync(int id);
    }
}
