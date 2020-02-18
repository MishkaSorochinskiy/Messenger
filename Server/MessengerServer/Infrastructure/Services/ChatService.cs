﻿using Application.IServices;
using Application.Models.ChatDto.Requests;
using Application.Models.ChatDto.Responces;
using Domain;
using Domain.Entities;
using Domain.Exceptions.UserExceptions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unit;

        private readonly AuthService _auth;

        private readonly IConfiguration _config;
        public ChatService(IUnitOfWork unit, AuthService auth,IConfiguration config)
        {
            _unit = unit;

            _auth = auth;

            _config = config;
        }

        public async Task<bool> CreateChatAsync(AddChatRequest request)
        {
            var user = await _auth.FindByNameUserAsync(request.UserName);

            if ((await this._unit.ChatRepository.ChatExistAsync(user.Id, request.SecondUserId)))
            {
                var chat = new Chat()
                {
                    FirstUserId = user.Id,
                    SecondUserId = request.SecondUserId
                };

                var grettingMessage = new Message()
                {
                    Content = _config.GetValue<string>("greetmessage"),
                    TimeCreated = DateTime.Now,
                    UserId = user.Id,
                    Chat = chat
                };

                await this._unit.MessageRepository.CreateAsync(grettingMessage);

                await this._unit.Commit();

                chat.LastMessage = grettingMessage;

                await this._unit.Commit();

                return true;
            }

            return false;
        }

        public async Task<List<GetChatDto>> GetChatsAsync(GetChatsRequestDto request)
        {
            var user = await this._unit.UserRepository.GetUserWithBlackList(request.UserName);

            if (user == null)
                throw new UserNotExistException("Given user not exist!",400);

            var chatres= await _unit.ChatRepository.GetUserChatsAsync(user.Id);

            var res = new List<GetChatDto>();

            foreach(var chat in chatres)
            {
                res.Add(new GetChatDto()
                {
                    Id = chat.Id,
                    Photo = chat.FirstUserId == user.Id ? chat.SecondUser.Photo.Name : chat.FirstUser.Photo.Name,
                    Content = chat.LastMessage == null ? null : chat.LastMessage.Content,
                    SecondUserId = chat.FirstUserId == user.Id ? chat.SecondUserId : chat.FirstUserId,
                    IsBlocked = user.BlockedUsers.Any(bl => bl.UserToBlockId == chat.SecondUserId || bl.UserToBlockId==chat.FirstUserId) ? true : false
                }) ;
            }

            return res;
        }
        
    }
}
