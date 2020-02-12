using Application.IServices;
using Application.Models.ChatDto.Requests;
using Application.Models.MessageDto;
using Application.Models.UserDto;
using AutoMapper;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MessageService:IMessageService
    {
        private readonly IUnitOfWork _unit;

        private readonly AuthService _auth;

        private readonly IMapper _map;

        public MessageService(IUnitOfWork unit,AuthService auth,IMapper map)
        {
            _unit = unit;

            _auth = auth;

            _map = map;
        }

        public async Task<GetMessageDto> AddMessageAsync(AddMessageDto message)
        {
            var user = await _auth.FindByNameUserAsync(message.UserName);

            var chat = await _unit.ChatRepository.GetAsync(message.chatId);

            if (user != null & !string.IsNullOrEmpty(message.Content)&& chat!=null)
            {
                var newmessage= new Message()
                {
                    Content = message.Content,
                    TimeCreated = DateTime.Now,
                    UserId = user.Id,
                    ChatId=message.chatId
                };

                user.Messages.Add(newmessage);

                chat.LastMessage = newmessage;

                await _unit.Commit();

                return _map.Map<GetMessageDto>(newmessage);
            }

            return default(GetMessageDto);
        }

        public async Task<AllMessagesDto> GetAllMessagesAsync()
        {
            var messages = await _unit.MessageRepository.GetAllWithUsersAsync();

            var users = messages.Distinct(new MessageComparer()).Select(m=>m.User);

            var result = new AllMessagesDto()
            {
                Users = _map.Map<List<GetUserDto>>(users),

                Messages = _map.Map<List<GetMessageDto>>(messages)
            };

            return result;
        }

        public async Task<AllMessagesDto> GetMessageByChatAsync(GetChatMessagesRequest request)
        {
           var chatContent= await this._unit.ChatRepository.GetChatContentAsync(request.Id);

            var result = new AllMessagesDto()
            {
                Users = _map.Map<List<GetUserDto>>(new List<User>() { chatContent.FirstUser, chatContent.SecondUser }),
                Messages = _map.Map<List<GetMessageDto>>(chatContent.Messages.OrderBy(m => m.TimeCreated))
            };

            return result;
        }
    }
}
