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
        private readonly UserManager<SecurityUser> _userManager;
        
        private readonly RoleManager<IdentityRole> _roleManager;
        
        private readonly SignInManager<SecurityUser> _signInManager;
       
        private readonly MessengerContext _db;
       
        private readonly IConfiguration _config;

        public AuthService(UserManager<SecurityUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<SecurityUser> signInManager,MessengerContext db,IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _db = db;
            _config = config;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> SignIn(LoginModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
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

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Chatter");
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            }

            return result;
        }

        public async Task<SecurityUser> FindByNameAsync(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }

        public async Task<User> FindByNameUserAsync(string name)
        {
            var secuser= await _userManager.FindByNameAsync(name);

            if (secuser != null)
            {
                return await _db.Users.FindAsync(secuser.UserId);
            }

            return default(User);
        }
    }
}
