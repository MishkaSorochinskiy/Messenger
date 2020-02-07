using Application.IServices;
using Application.Models.UserDto;
using AutoMapper;
using Domain;
using Domain.Entities;
using Infrastructure.AppSecurity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService:IUserService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _map;
        private readonly AuthService _auth;
        private readonly SecurityContext _seccontext;
        public UserService(IUnitOfWork unit,IMapper map,AuthService auth,SecurityContext seccontext)
        {
            _unit = unit;

            _auth = auth;

            _map = map;

            _seccontext = seccontext;
        }

        public async Task<GetUserDto> GetUserInfo(GetUserInfoRequest request)
        {
            var user= await _unit.UserRepository.GetWithPhoto(request.UserId);

            if (user != null)
            {
                return _map.Map<GetUserDto>(user);
            }

            return default(GetUserDto);
        }

        public async Task<bool> UpdateUser(UpdateUserDto model)
        {
            var user = await _auth.FindByNameUserAsync(model.Email);

            if (user != null)
            {
                user.Age = model.Age;

                user.PhoneNumber = model.Phone;

                user.NickName = model.NickName;

                await _unit.Commit();

                return true;
            }

            return false;
        }

        public  async Task<List<SearchUserDto>> SearchUser(SearchUserDtoRequest request)
        {
            var currentuser = await _auth.FindByNameUserAsync(request.UserName);

            var users = (await _unit.UserRepository.Search(request.Filter));

            users.Remove(currentuser);

            var res = _map.Map<List<SearchUserDto>>(users);

            return res;
        }
    }
}
