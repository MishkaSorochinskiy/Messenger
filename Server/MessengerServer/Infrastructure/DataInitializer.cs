using Domain;
using Domain.Entities;
using Infrastructure.AppSecurity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DataInitializer
    {
        public static async Task SeedData(UserManager<SecurityUser> userManager, RoleManager<IdentityRole> roleManager, SecurityContext context,IUnitOfWork unit)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager, context,unit);
        }
        public static async Task SeedUsers(UserManager<SecurityUser> userManager, SecurityContext context,IUnitOfWork unit)
        {
            string username = "admin@gmail.com";
            string password = "mainadmin";
            if (await userManager.FindByNameAsync(username) == null)
            {
                User admin = new User();
                await unit.UserRepository.Create(admin);
                await unit.Commit();

                SecurityUser secadmin = new SecurityUser() { UserName=username,Email=username,UserId=admin.Id};
                IdentityResult result = await userManager.CreateAsync(secadmin, password);
                
                if (result.Succeeded)
                {
                    await context.SaveChangesAsync();
                    await userManager.AddToRoleAsync(secadmin, "Admin");
                    await userManager.AddToRoleAsync(secadmin, "Chatter");
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
