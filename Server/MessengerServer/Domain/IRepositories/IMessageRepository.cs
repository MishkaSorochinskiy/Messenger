﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IMessageRepository:IRepository<Message>
    {
        Task<IEnumerable<Message>> GetAllWithUsersAsync();
    }
}
