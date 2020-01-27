using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.AppSecurity
{
    public class SecurityContext:IdentityDbContext<SecurityUser>
    {
        public SecurityContext(DbContextOptions<SecurityContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SecurityUser>().Ignore(su => su.User);
        }
    }
}
