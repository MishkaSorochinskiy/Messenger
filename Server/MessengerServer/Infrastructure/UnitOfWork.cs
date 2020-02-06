using Domain;
using Domain.IRepositories;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork:IUnitOfWork
    {
        public MessengerContext _db { get; private set; }

        private IUserRepository userRepository;      
        private IPhotoRepository photoRepository;     
        private IMessageRepository messageRepository;
        private IChatRepository chatRepository;
        private IBlockedUserRepository blockeduserRepository;

        public IChatRepository ChatRepository
        {
            get
            {
                if (chatRepository == null)
                {
                    chatRepository = new ChatRepository(_db);
                }

                return chatRepository;
            }
        }
        public IBlockedUserRepository BlockedUserRepository
        {
            get
            {
                if (blockeduserRepository == null)
                {
                    blockeduserRepository = new BlockedUserRepository(_db);
                }

                return blockeduserRepository;
            }
        }
        public IPhotoRepository PhotoRepository
        {
            get
            {
                if (photoRepository == null)
                {
                    photoRepository = new PhotoRepository(_db);
                }

                return photoRepository;
            }
        }
        public IMessageRepository MessageRepository
        {
            get
            {
                if (messageRepository == null)
                {
                    messageRepository = new MessageRepository(_db);
                }

                return messageRepository;
            }
        }
        public IUserRepository UserRepository 
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(_db);
                }

                return UserRepository;
            }
        }


        public UnitOfWork(MessengerContext db)
        {
            _db = db;
        }

        public async Task Commit()
        {
            await _db.SaveChangesAsync();
        }
    }
}
