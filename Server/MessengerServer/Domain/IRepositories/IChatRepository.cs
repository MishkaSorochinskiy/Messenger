using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IChatRepository:IRepository<Chat>
    {
        Task<bool> ChatExist(int firstUserId, int secondUserId);

        Task<List<Chat>> GetUserChats(int userid);

        Task<Chat> GetChatContent(int id);
    }
}
