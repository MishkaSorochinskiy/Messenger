using Application.IServices;
using Application.Models.UserDto;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService:IUserService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _map;
        private readonly AuthService _auth;
        public UserService(IUnitOfWork unit,IMapper map,AuthService auth)
        {
            _unit = unit;

            _auth = auth;

            _map = map;
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
    }
}
