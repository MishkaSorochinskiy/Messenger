using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IUnitOfWork
    {
        IConversationInfoRepository ConversationInfoRepository { get; }
        IMessageRepository MessageRepository { get; }
        IUserRepository UserRepository { get; }
        IConversationRepository ConversationRepository { get; }
        IBlockedUserRepository BlockedUserRepository { get; }
        IUserConversationRepository UserConversationRepository { get; }
       
        Task Commit();
    }
}
