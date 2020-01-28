using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class PhotoRepository:Repository<Photo>,IPhotoRepository
    {
        public PhotoRepository(MessengerContext db):base(db)
        {

        }
    }
}
