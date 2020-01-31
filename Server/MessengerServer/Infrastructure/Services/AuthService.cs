using Application.Models;
using Domain.Entities;
using Infrastructure.AppSecurity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        IConfiguration _config;

        public AuthService(UserManager<SecurityUser> usermanager, RoleManager<IdentityRole> rolemanager,
            SignInManager<SecurityUser> signinmanager,MessengerContext db,IConfiguration config)
        {
            _usermanager = usermanager;
            _rolemanager = rolemanager;
            _signinmanager = signinmanager;
            _db = db;
            _config = config;
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
            var appuser = new User() 
            {
                NickName=model.NickName,
                Age=model.Age,
                PhoneNumber=model.PhoneNumber,
                Sex=model.Sex
            };

            await _db.Users.AddAsync(appuser);
            await _db.SaveChangesAsync();

            var photo = new Photo()
            {
                UserId=appuser.Id,
                Path=$"{_config.GetValue<string>("defaultimagepath")}{(model.Sex == Sex.Male ? "defaultmale.png":"defaultfemale.png")}",
                Name= model.Sex==Sex.Male? _config.GetValue<string>("defaultmale"): _config.GetValue<string>("defaultfemale")
            };

            await _db.Photos.AddAsync(photo);
            await _db.SaveChangesAsync();

            SecurityUser user = new SecurityUser();
            user.Email = model.Email;
            user.UserName = model.Email;
            user.UserId = appuser.Id;

            IdentityResult result = await _usermanager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _usermanager.AddToRoleAsync(user, "Chatter");
                await _signinmanager.PasswordSignInAsync(model.Email, model.Password, false, false);
            }

            return result;
        }

        public async Task<SecurityUser> FindByNameAsync(string name)
        {
            return await _usermanager.FindByNameAsync(name);
        }

        public async Task<User> FindByNameUserAsync(string name)
        {
            var secuser= await _usermanager.FindByNameAsync(name);

            if (secuser != null)
            {
                return await _db.Users.FindAsync(secuser.UserId);
            }

            return default(User);
        }
    }
}
