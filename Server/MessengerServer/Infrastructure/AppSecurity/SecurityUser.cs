using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.AppSecurity
{
    public class SecurityUser:IdentityUser<int>
    {
    }
}
