using Application.IServices;
using Application.Models.ChatDto.Requests;
using Application.Models.ChatDto.Responces;
using Domain;
using Domain.Entities;
using Domain.Exceptions.ChatExceptions;
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

        private readonly IAuthService _auth;

        private readonly IConfiguration _config;
        public ChatService(IUnitOfWork unit, IAuthService auth,IConfiguration config)
        {
            _unit = unit;

            _auth = auth;

            _config = config;
        }

        public async Task CreateChatAsync(AddChatRequest request)
        {
            var user = await _auth.FindByIdUserAsync(request.userId);

            if (user == null)
                throw new UserNotExistException("user not exist", 400);

            if ((await this._unit.ChatRepository.ChatExistAsync(user.Id, request.SecondUserId)))
            {
                var grettingMessage = new Message()
                {
                    Content = _config.GetValue<string>("greetmessage"),
                    TimeCreated = DateTime.Now,
                    UserId = user.Id,
                };

                var chat = new Conversation()
                {
                    FirstUserId = user.Id,
                    SecondUserId = request.SecondUserId,
                    LastMessage = grettingMessage
                };

                await this._unit.ChatRepository.CreateAsync(chat);

                await this._unit.Commit();
            }
            else
            {
                throw new ChatAlreadyExistException("chat already exist", 400);
            }
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
