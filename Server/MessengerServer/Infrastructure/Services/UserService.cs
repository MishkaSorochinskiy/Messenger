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
    }
}
