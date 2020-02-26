using Application.Models;
using Domain;
using Domain.Entities;
using Infrastructure.AppSecurity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IAuthService
    {
        Task SignOutAsync();

        Task<SignInResult> SignInAsync(LoginModel model);

        Task<IdentityResult> RegisterAsync(RegisterModel model);

        Task<bool> EmailExistAsync(CheckRegisterModel model);

        Task<User> FindByIdUserAsync(int id);
    }

    public class AuthService:IAuthService
    {
        private readonly UserManager<SecurityUser> _userManager;
        
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        
        private readonly SignInManager<SecurityUser> _signInManager;
       
        private readonly IUnitOfWork _unit;

        private readonly IConfiguration _config;

        public AuthService(UserManager<SecurityUser> userManager, RoleManager<IdentityRole<int>> roleManager,
            SignInManager<SecurityUser> signInManager,IUnitOfWork unit,IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _unit = unit;
            _config = config;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> SignInAsync(LoginModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        }

        public async Task<IdentityResult> RegisterAsync(RegisterModel model)
        {
            SecurityUser user = new SecurityUser();
            user.Email = model.Email;
            user.UserName = model.Email;

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Chatter");
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                var appUser = new User()
                {
                    NickName = model.NickName,
                    Age = model.Age,
                    PhoneNumber = model.PhoneNumber,
                    Sex = model.Sex,
                    Email = model.Email,
                    Id = user.Id
                };

                var photo = new Photo()
                {
                    UserId = user.Id,
                    Path = $"{_config.GetValue<string>("defaultimagepath")}{(model.Sex == Sex.Male ? "defaultmale.png" : "defaultfemale.png")}",
                    Name = model.Sex == Sex.Male ? _config.GetValue<string>("defaultmale") : _config.GetValue<string>("defaultfemale")
                };

                await _unit.UserRepository.CreateAsync(appUser);

                await _unit.PhotoRepository.CreateAsync(photo);

                await _unit.Commit();
            }

            return result;
        }

        public async Task<User> FindByIdUserAsync(int id)
        {
            return await _unit.UserRepository.GetAsync(id);
        }

        public async Task<bool> EmailExistAsync(CheckRegisterModel model)
        {
            return await _userManager.FindByEmailAsync(model.Email) == null;
        }
    }
}
