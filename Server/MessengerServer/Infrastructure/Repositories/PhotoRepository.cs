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
    public class PhotoRepository:Repository<Photo>,IPhotoRepository
    {
        public PhotoRepository(MessengerContext db):base(db)
        {

        }

        public async Task<Photo> GetPhotoByUserAsync(int userid)
        {
            return await this.db.Photos.Where(p => p.UserId == userid)
                .FirstOrDefaultAsync();
        }
    }
}
