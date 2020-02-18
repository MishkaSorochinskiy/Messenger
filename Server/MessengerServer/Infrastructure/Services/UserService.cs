﻿using Application.IServices;
using Application.Models.MessageDto;
using Application.Models.UserDto;
using Application.Models.UserDto.Requests;
using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.Exceptions.BlockedUserExceptions;
using Domain.Exceptions.ChatExceptions;
using Domain.Exceptions.UserExceptions;
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
            var user= await _unit.UserRepository.GetWithPhotoAsync(request.UserName);

            if (user == null)
                throw new UserNotExistException("Given user not exist!!",400);
            
            return _map.Map<GetUserDto>(user);
        }

        public async Task UpdateUserAsync(UpdateUserDto model)
        {
            var user = await _auth.FindByNameUserAsync(model.Email);

            if (user == null)
                throw new UserNotExistException("Given user not exist!!", 400);
      
            user.Age = model.Age;

            user.PhoneNumber = model.Phone;

            user.NickName = model.NickName;

            await _unit.Commit();         
        }

        public  async Task<List<SearchUserDto>> SearchUserAsync(SearchUserDtoRequest request)
        {
            var currentUser = await _auth.FindByNameUserAsync(request.UserName);

            if (currentUser == null)
                throw new UserNotExistException("Given user not exist!!", 400);

            var users = (await _unit.UserRepository.SearchUsersAsync(request.Filter));

            users.Remove(currentUser);

            var res = _map.Map<List<SearchUserDto>>(users);

            return res;
        }

        public async Task BlockUserAsync(BlockUserRequest request) 
        {
            var currentUser = await this._auth.FindByNameUserAsync(request.UserName);

            if (currentUser == null)
                throw new UserNotExistException("Given user not exist!!",400);
            
            var userToBlock = await this._unit.UserRepository.GetAsync(request.UserIdToBlock);

            if (userToBlock == null)
                throw new UserNotExistException("User to block not exist!!", 400);


            var blockedUser = await this._unit.BlockedUserRepository
                              .IsBlockedUserAsync(currentUser.Id, request.UserIdToBlock);

            if (blockedUser != null)
                throw new BlockedUserAlreadyExistException("Given user to block is already blocked!!",400);

            
            var newBlockedUser = new BlockedUser()
            {
                UserId = currentUser.Id,
                UserToBlockId = request.UserIdToBlock
            };

            await this._unit.BlockedUserRepository
                    .CreateAsync(newBlockedUser);

            await this._unit.Commit();
        }

        public async Task UnBlockUserAsync(BlockUserRequest request)
        {
            var currentUser = await this._auth.FindByNameUserAsync(request.UserName);

            if (currentUser == null)
                throw new UserNotExistException("Given user not exist!!", 400);

            var blockedUser = await this._unit.BlockedUserRepository
                              .IsBlockedUserAsync(currentUser.Id, request.UserIdToBlock);

            if (blockedUser == null)
                throw new UserNotExistException("User to unblock not exist!!", 400);

            
            await this._unit.BlockedUserRepository.DeleteAsync(blockedUser.Id);

            await this._unit.Commit();
        }

        public async Task<bool> CheckStatusAsync(AddMessageDto request)
        {
            var chat = await this._unit.ChatRepository.GetAsync(request.chatId);

            if (chat == null)
                throw new ChatNotExistException("Given chat not exist!!",400);

            var currentUser = await this._auth.FindByNameUserAsync(request.UserName);

            if (currentUser == null)
                throw new UserNotExistException("Given user not exist!!",400);

            
            var requestedUserId = chat.FirstUserId == currentUser.Id ? chat.SecondUserId : chat.FirstUserId;

            var requestedUser = await this._unit.UserRepository.GetAsync(requestedUserId);

            var requestUserBlackList = await this._unit.UserRepository.GetUserWithBlackList(requestedUser.Email);

            if (requestUserBlackList.BlockedUsers.Any(bl => bl.UserToBlockId == currentUser.Id))
            {
                return false;
            }

            return true;
        }
    }
}
