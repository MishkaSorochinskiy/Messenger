using Application.Models;
using Domain.Entities;
using Infrastructure.AppSecurity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthService
    {
        UserManager<SecurityUser> _usermanager;
        RoleManager<IdentityRole> _rolemanager;
        SignInManager<SecurityUser> _signinmanager;
        MessengerContext _db;

        public AuthService(UserManager<SecurityUser> usermanager, RoleManager<IdentityRole> rolemanager,
            SignInManager<SecurityUser> signinmanager,MessengerContext db)
        {
            _usermanager = usermanager;
            _rolemanager = rolemanager;
            _signinmanager = signinmanager;
            _db = db;
        }

        public async Task SignOut()
        {
            await _signinmanager.SignOutAsync();
        }

        public async Task<SignInResult> SignIn(LoginModel model)
        {
            return await _signinmanager.PasswordSignInAsync(model.Email, model.Password, false, false);
        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var appuser = new User();
            await _db.Users.AddAsync(appuser);
            await _db.SaveChangesAsync();

            SecurityUser user = new SecurityUser();
            user.Email = model.Email;
            user.UserName = model.Email;
            user.UserId = appuser.Id;

            IdentityResult result = await _usermanager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //await _usermanager.AddToRoleAsync(user, "Worker");
                await _signinmanager.PasswordSignInAsync(model.Email, model.Password, false, false);
            }

            return result;
        }

        public async Task<SecurityUser> FindByNameAsync(string name)
        {
            return await _usermanager.FindByNameAsync(name);
        }
    }
}
