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
    public class UserRepository:Repository<User>,IUserRepository
    {
        public UserRepository(MessengerContext db):base(db)
        {
            
        }
    }
}
