using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class BlockedUserRepository:Repository<BlockedUser>,IBlockedUserRepository
    {
        public BlockedUserRepository(MessengerContext db):base(db)
        {

        }
    }
}
