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
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(MessengerContext db):base(db)
        {

        }

        public IEnumerable<Message> GetAllWithUsers()
        {
           return this.db.Messages.Include(m => m.User)
                .ThenInclude(u=>u.Photo);
        }
    }
}
