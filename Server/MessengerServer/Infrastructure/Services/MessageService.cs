using Application.IServices;
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

        public async Task<GetMessageDto> AddMessage(AddMessageDto message)
        {
            var user = await _auth.FindByNameUserAsync(message.UserName);

            if (user != null & !string.IsNullOrEmpty(message.Content))
            {
                var newmessage= new Message()
                {
                    Content = message.Content,
                    TimeCreated = DateTime.Now,
                    UserId = user.Id
                };

                user.Messages.Add(newmessage);

                await _unit.Commit();

                return _map.Map<GetMessageDto>(newmessage);
            }

            return default(GetMessageDto);
        }

        public AllMessagesDto GetAllMessages()
        {
            var messages = _unit.MessageRepository.GetAllWithUsers()
                .ToList()
                .OrderBy(m => m.TimeCreated);

            var users = messages.Distinct(new MessageComparer()).Select(m=>m.User);

            var result = new AllMessagesDto()
            {
                Users = _map.Map<List<GetUserDto>>(users),

                Messages = _map.Map<List<GetMessageDto>>(messages)
            };

            return result;
        }
    }
}
