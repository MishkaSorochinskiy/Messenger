﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> GetWithPhoto(int id);
    }
}
