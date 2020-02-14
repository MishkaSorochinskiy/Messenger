using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IUnitOfWork
    {
        IPhotoRepository PhotoRepository { get; }
        IMessageRepository MessageRepository { get; }
        IUserRepository UserRepository { get; }
        IChatRepository ChatRepository { get; }
        IBlockedUserRepository BlockedUserRepository { get; }
       
        Task Commit();
    }
}
