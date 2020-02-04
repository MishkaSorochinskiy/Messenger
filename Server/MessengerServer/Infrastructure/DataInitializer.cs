using Domain;
using Domain.Entities;
using Infrastructure.AppSecurity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DataInitializer
    {
        public static async Task SeedData(UserManager<SecurityUser> userManager, RoleManager<IdentityRole> roleManager, SecurityContext context,MessengerContext mescontext,IConfiguration config)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager, context,mescontext,config);
        }
        public static async Task SeedUsers(UserManager<SecurityUser> userManager, SecurityContext context,MessengerContext mescontext,IConfiguration _config)
        {
            string username = "admin@gmail.com";
            string password = "mainadmin";
            if (await userManager.FindByNameAsync(username) == null)
            {
                 User admin = new User() {NickName="admin_captain",Sex=Sex.Male};
                 mescontext.Users.Add(admin);
                 mescontext.SaveChanges();

                var photo = new Photo()
                {
                    UserId = admin.Id,
                    Path = _config.GetValue<string>("defaultimagepath"),
                    Name = _config.GetValue<string>("defaultimagename")
                };

                mescontext.SaveChanges();

                SecurityUser secadmin = new SecurityUser() { UserName=username,Email=username,UserId=admin.Id};
                IdentityResult result = await userManager.CreateAsync(secadmin, password);
                
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(secadmin, "Admin");
                    await userManager.AddToRoleAsync(secadmin, "Chatter");
                    await context.SaveChangesAsync();
                }
            }
        }
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Chatter", "Admin" };
            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (roleExist == false)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
