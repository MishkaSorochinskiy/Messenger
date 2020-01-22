using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserRepository:Repository<User>,IUserRepository
    {
        public UserRepository(MessengerContext db):base(db)
        {

        }
    }
}
