using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.AppSecurity
{
    public class SecurityUser:IdentityUser
    {
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
