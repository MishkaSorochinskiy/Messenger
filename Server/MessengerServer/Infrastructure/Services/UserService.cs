using Application.IServices;
using Application.Models.UserDto;
using Application.Models.UserDto.Requests;
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
        private readonly SecurityContext _secContext;
        public UserService(IUnitOfWork unit,IMapper map,AuthService auth,SecurityContext secContext)
        {
            _unit = unit;

            _auth = auth;

            _map = map;

            _secContext = secContext;
        }

        public async Task<GetUserDto> GetUserInfoAsync(GetUserInfoRequest request)
        {
            var user= await _unit.UserRepository.GetWithPhotoAsync(request.UserId);

            if (user != null)
            {
                return _map.Map<GetUserDto>(user);
            }

            return default(GetUserDto);
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto model)
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

        public  async Task<List<SearchUserDto>> SearchUserAsync(SearchUserDtoRequest request)
        {
            var currentUser = await _auth.FindByNameUserAsync(request.UserName);

            var users = (await _unit.UserRepository.SearchUsersAsync(request.Filter));

            users.Remove(currentUser);

            var res = _map.Map<List<SearchUserDto>>(users);

            return res;
        }

        public async Task<bool> BlockUserAsync(BlockUserRequest request) 
        {
            var currentUser = await this._auth.FindByNameUserAsync(request.UserName);
            
            var userToBlock = await this._unit.UserRepository.GetAsync(request.UserIdToBlock);


            var blockedUser = await this._unit.BlockedUserRepository
                              .IsBlockedUserAsync(currentUser.Id, request.UserIdToBlock);

            if (blockedUser == null && userToBlock!=null)
            {
                var newBlockedUser = new BlockedUser()
                {
                    UserId = currentUser.Id,
                    UserToBlockId = request.UserIdToBlock
                };

                await this._unit.BlockedUserRepository
                        .CreateAsync(newBlockedUser);

                await this._unit.Commit();

                return true;
            }

            return false;
        }

        public async Task<bool> UnBlockUserAsync(BlockUserRequest request)
        {
            var currentUser = await this._auth.FindByNameUserAsync(request.UserName);

            var blockedUser = await this._unit.BlockedUserRepository
                              .IsBlockedUserAsync(currentUser.Id, request.UserIdToBlock);

            if (blockedUser != null)
            {
                await this._unit.BlockedUserRepository.DeleteAsync(blockedUser.Id);

                await this._unit.Commit();

                return true;
            }

            return false;
        }
    }
}
